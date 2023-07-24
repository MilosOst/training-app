using Microsoft.EntityFrameworkCore;
using TrainingApp.Data;
using TrainingApp.Errors;
using TrainingApp.Features.Users;

namespace TrainingApp.Features.Authentication;

public interface IAuthenticationService
{
    Task RegisterUser(RegisterUserRequest req);
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
}