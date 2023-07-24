using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Features.Users;

public class RegisterUserRequest
{
    [Required]
    [MinLength(6, ErrorMessage = "Username must be between 6 and 25 characters long.")]
    [MaxLength(25, ErrorMessage = "Username must be between 6 and 25 characters long.")]
    [RegularExpression(@"^\w+$", ErrorMessage = "Username must consist of only underscores and alphanumeric characters.")]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address provided.")]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Username must be at least 8 characters long.")]
    public string Password { get; set; }
}