using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Features.Trainings.Models;

public class CreateDateRequest
{
    [Required]
    public DateOnly ScheduledDate { get; set; }

}