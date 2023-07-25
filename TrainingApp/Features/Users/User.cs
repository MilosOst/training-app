using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrainingApp.Features.Authentication.Models;

namespace TrainingApp.Features.Users;

public class User
{
    [Key, Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(25, ErrorMessage = "Username must be between 6 and 25 characters long.")]
    [RegularExpression(@"^\w+$",
        ErrorMessage = "Username must consist of only underscores and alphanumeric characters.")]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address provided.")]
    public string Email { get; set; } = null!;

    [Required]
    [JsonIgnore]
    public string Password { get; private set; } = null!;

    [Required]
    public bool IsVerified { get; set; } = false;
    
    public ICollection<UserSession> UserSessions { get; set; }

    public void SetPassword(string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        this.Password = passwordHash;
    }

    public bool VerifyPassword(string comparePassword)
    {
        return BCrypt.Net.BCrypt.Verify(comparePassword, this.Password);
    }
}