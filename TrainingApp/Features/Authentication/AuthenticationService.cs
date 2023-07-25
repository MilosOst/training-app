using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Data;
using TrainingApp.Exceptions;
using TrainingApp.Features.Authentication.Models;
using TrainingApp.Features.Users;

namespace TrainingApp.Features.Authentication;

public interface IAuthenticationService
{
    Task RegisterUser(RegisterUserRequest req);
    
    /// <summary>
    /// Attempt to login the user with the given credentials.
    /// </summary>
    /// <param name="emailOrUsername">User email/username</param>
    /// <param name="password">User password</param>
    /// <returns>A new randomly generated SessionId for the user to use in future requests.</returns>
    Task<string> LoginUser(string emailOrUsername, string password);
}

public class AuthenticationService: IAuthenticationService
{
    private readonly DataContext _db;

    public AuthenticationService(DataContext db)
    {
        _db = db;
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
}