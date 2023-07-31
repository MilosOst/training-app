using Microsoft.EntityFrameworkCore;
using TrainingApp.Data;
using TrainingApp.Exceptions;
using TrainingApp.Features.Trainings.Models;
using TrainingApp.Features.Trainings.UserTrainingDrills;

namespace TrainingApp.Features.Trainings;


public class TrainingService: ITrainingService
{
    private readonly DataContext _db;

    public TrainingService(DataContext db)
    {
        _db = db;
    }

    public async Task RegisterTraining(CreateTrainingRequest req, string userId)
    {
        List<UserTrainingDrill> userTrainingDrills = new List<UserTrainingDrill>();
        Dictionary<string, List<String>> validationErrors = new Dictionary<string, List<string>>();

        for (int i = 0; i < req.Drills.Count; i++)
        {
            var drill = req.Drills[i];
            if (!_db.FixedDrills.Any(p => p.FixedDrillId == drill.DrillId))
            {
                validationErrors[$"Drills[{i}].DrillId"] = new List<string>() { "Invalid DrillId" };
            }

            userTrainingDrills.Add(new UserTrainingDrill()
            {
                Duration = drill.Duration,
                DrillId = drill.DrillId
            });
        }
        
        // Check if any validationErrors occured
        if (validationErrors.Count > 0)
        {
            throw new BadRequestException(validationErrors);
        }

        
        var newTraining = new UserTraining()
        {
            Age = req.Age,
            Date = req.ScheduledDate,
            UserTrainingDrills = userTrainingDrills,
            UserId = new Guid(userId)
        };
        
        _db.UserTrainings.Add(newTraining);
        await _db.SaveChangesAsync();
    }
}