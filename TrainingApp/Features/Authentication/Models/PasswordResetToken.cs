using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingApp.Features.Users;

namespace TrainingApp.Features.Authentication.Models;

public class PasswordResetToken
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [Required]
    public string Token { get; private set; }

    [Required]
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime ExpirationTime { get; set; } = DateTime.UtcNow.AddHours(1);

    [Required]
    public User User { get; set; } = null!;

    public void SetToken(string token)
    {
        string tokenHash = BCrypt.Net.BCrypt.HashPassword(token);
        this.Token = tokenHash;
    }

    public bool CompareToken(string token)
    {
        return BCrypt.Net.BCrypt.Verify(token, this.Token);
    }
}