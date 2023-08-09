using TrainingApp.Features.Authentication.Models;

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

    /// <summary>
    /// Request a password reset for the account associated with the email provided.
    /// </summary>
    /// <param name="email">Email associated with account</param>
    Task RequestPasswordReset(string email);

    /// <summary>
    /// Change the user's password to the provided one given a valid resetToken and userId have been provided.
    /// </summary>
    /// <param name="newPassword">New password to use</param>
    /// <param name="userId">User Id</param>
    /// <param name="resetToken">Reset token provided when password reset was requested</param>
    /// <returns></returns>
    Task ResetPassword(string newPassword, Guid userId, string resetToken);
}