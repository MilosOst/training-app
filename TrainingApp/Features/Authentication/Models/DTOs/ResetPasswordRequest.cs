using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Features.Authentication.Models;

public class ResetPasswordRequest
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public string Token { get; set; }
    
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; }
    
    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string PasswordConfirmation { get; set; }
}