using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Features.Authentication.Models;

public class LoginUserRequest
{
    [Required(ErrorMessage = "You must provide your email/username.")]
    public string EmailOrUsername { get; set; }
    
    [Required(ErrorMessage = "You must provide your password.")]
    public string Password { get; set; }
}