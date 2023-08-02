using TrainingApp.Features.Trainings.Models;

namespace TrainingApp.Features.Trainings;

public interface ITrainingService
{
    Task RegisterTraining(CreateTrainingRequest req, string userId);

    Task<List<SendTraining>> TrainingHistory(CreateDateRequest req, string userId);
}