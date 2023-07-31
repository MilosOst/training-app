using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Features.Trainings.Models;

public class CreateTrainingRequest
{
    [Required]
    public AgeGroup Age { get; set; }
    
    // no tarinings before todays date
    [Required]
    public DateOnly ScheduledDate { get; set; }
    
    [Required]
    public UserTrainingDrillInput[] DrillInputs { get; set; }
}

public class UserTrainingDrillInput {
    
    [Required]
    [Range(1, 99)]
    public int Duration { get; set; }
    
    [Required]
    public int DrillId { get; set; }
    
    
}