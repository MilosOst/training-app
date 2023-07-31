using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Features.Trainings.Models;

public class CreateTrainingRequest
{
    [Required]
    public AgeGroup Age { get; set; }
    
    [Required]
    public DateOnly ScheduledDate { get; set; }
    
    [Required]
    public List<UserTrainingDrillInput> Drills { get; set; }
}

public class UserTrainingDrillInput {
    
    [Required]
    [Range(1, 99)]
    public int Duration { get; set; }
    
    [Required]
    public int DrillId { get; set; }
}