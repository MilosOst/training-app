using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingApp.Features.Users;

namespace TrainingApp.Features.Authentication.Models;

public class UserSession
{
    [Key, Required]
    public Guid SessionId { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    public User User { get; set; } = null!;
}