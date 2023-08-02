using TrainingApp.Features.Trainings.UserTrainingDrills;

namespace TrainingApp.Features.Trainings.Models;

public class SendTraining
{
    public AgeGroup Age { get; set; }
    public DateOnly Date { get; set; }
    public ICollection<UserTrainingDrill> UserTrainingDrills { get; set; }
}