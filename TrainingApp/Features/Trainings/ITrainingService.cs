using TrainingApp.Features.Trainings.Models;

namespace TrainingApp.Features.Trainings;

public interface ITrainingService
{
    Task RegisterTraining(CreateTrainingRequest req, string userId);

    Task<List<UserTrainingDto>> GetTrainingHistory(DateOnly forDate, string userId);

    Task UpdateTraining(CreateTrainingRequest req, string userId, int TrainingId);

    Task DeleteTraining(string userId, int TrainingId);
}