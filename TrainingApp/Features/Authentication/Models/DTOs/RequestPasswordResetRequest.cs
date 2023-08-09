using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Features.Authentication.Models;

public class RequestPasswordResetRequest
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address provided.")]
    public string Email { get; set; }
}