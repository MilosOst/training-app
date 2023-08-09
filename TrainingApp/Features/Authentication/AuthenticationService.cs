using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Data;
using TrainingApp.Exceptions;
using TrainingApp.Features.Authentication.Models;
using TrainingApp.Features.Mail;
using TrainingApp.Features.Users;

namespace TrainingApp.Features.Authentication;

public class AuthenticationService: IAuthenticationService
{
    private readonly DataContext _db;
    private readonly IMailService _mailService;

    public AuthenticationService(DataContext db, IMailService mailService)
    {
        _db = db;
        _mailService = mailService;
    }

    public async Task RegisterUser(RegisterUserRequest req)
    {
        // Username, email are unique
        var usernameTaken = await _db.Users.FirstOrDefaultAsync(p => p.Username.ToLower() == req.Username.ToLower());
        if (usernameTaken is not null)
        {
            throw new ConflictException("This username is already taken.");
        }

        var emailTaken = await _db.Users.FirstOrDefaultAsync(p => p.Email.ToLower() == req.Email.ToLower());
        if (emailTaken is not null)
        {
            throw new ConflictException("This email is already taken.");
        }

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Email = req.Email,
            Username = req.Username
        };
        
        newUser.SetPassword(req.Password);

        await _db.Users.AddAsync(newUser);
        await _db.SaveChangesAsync();
    }

    
    public async Task<string> LoginUser(string emailOrUsername, string password)
    {
        User? user;
        if (new EmailAddressAttribute().IsValid(emailOrUsername))
        {
            user = await _db.Users.FirstOrDefaultAsync(p => p.Email.ToLower() == emailOrUsername.ToLower());
        }
        else
        {
            user = await _db.Users.FirstOrDefaultAsync(p => p.Username.ToLower() == emailOrUsername.ToLower());
        }
        
        if (user is null || !user.VerifyPassword(password))
        {
            throw new UnauthorizedException("Email/Password is incorrect.");
        }
        
        // Confirm that user's account has been verified
        if (!user.IsVerified)
        {
            throw new UnauthorizedException("Your account has not been verified yet. Please wait for verification.");
        }
        
        // Credentials are valid, create new session
        Guid sessionId = Guid.NewGuid();
        await _db.UserSessions.AddAsync(new UserSession()
        {
            SessionId = sessionId,
            UserId = user.Id
        });
        await _db.SaveChangesAsync();

        return sessionId.ToString();
    }

    public async Task RequestPasswordReset(string email)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        if (user is not null && user.IsVerified) // User must also be verified to receive email
        {
            // Delete previous tokens if any exist
            await _db.PasswordResetTokens.Where(u => u.UserId == user.Id).ExecuteDeleteAsync();
            
            // Generate token
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32];
            rng.GetBytes(bytes);
            string token = BitConverter.ToString(bytes).Replace("-", "").ToLower();

            var resetToken = new PasswordResetToken()
            {
                UserId = user.Id,
            };

            resetToken.SetToken(token);
            await _db.PasswordResetTokens.AddAsync(resetToken);
            await _db.SaveChangesAsync();
            
            await _mailService.SendPasswordResetEmail(email, user.Username, user.Id.ToString(), token);
        }
        // Do nothing if user does not exist
    }

    public async Task ResetPassword(string newPassword, Guid userId, string resetToken)
    {
        // Verify token exists and not expired
        var token = await _db.PasswordResetTokens.FirstOrDefaultAsync(t => t.UserId == userId);
        if (token is null || DateTime.UtcNow > token.ExpirationTime || token.UserId != userId)
        {
            throw new BadRequestException("token",
                "Invalid/expired reset token provided. Please issue a reset request again.");
        }
        
        // Verify that token is valid
        if (!token.CompareToken(resetToken))
        {
            throw new BadRequestException("token",
                "Invalid/expired reset token provided. Please issue a reset request again.");
        }
        
        // Verify that password has changed and if true, update password
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
        {
            throw new InvalidOperationException($"User with userId: {userId} was not found.");
        }
        
        if (user.VerifyPassword(newPassword))
        {
            throw new BadRequestException("password", "You cannot reuse your current password.");
        }

        user.SetPassword(newPassword);
        await _db.SaveChangesAsync();
        
        // Delete all previous sessions and token
        _db.PasswordResetTokens.Remove(token);
        await _db.UserSessions.Where(u => u.UserId == userId).ExecuteDeleteAsync();
        await _db.SaveChangesAsync();
    }
}