using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingApp.Features.Users;

public class User
{
    // TODO: Consider changing this to string and label column as uuid?
    [Key, Required]
    public Guid Id { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Username must be between 6 and 25 characters long.")]
    [MaxLength(25, ErrorMessage = "Username must be between 6 and 25 characters long.")]
    [RegularExpression(@"^\w+$",
        ErrorMessage = "Username must consist of only underscores and alphanumeric characters.")]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address provided.")]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public bool IsVerified { get; set; } = false;

    public void SetPassword(string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        this.Password = passwordHash;
    }
}