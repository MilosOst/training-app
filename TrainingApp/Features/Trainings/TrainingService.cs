using Microsoft.EntityFrameworkCore;
using TrainingApp.Data;
using TrainingApp.Exceptions;
using TrainingApp.Features.Trainings.Models;
using TrainingApp.Features.Trainings.UserTrainingDrills;

namespace TrainingApp.Features.Trainings;


public class TrainingService : ITrainingService
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

    public async Task<List<UserTrainingDto>> GetTrainingHistory(DateOnly forDate, string userId)
    {
        return await _db.UserTrainings
            .Where(p => p.UserId.ToString() == userId && p.Date == forDate)
            .OrderByDescending(p => p.TrainingId)
            .Include(ut => ut.UserTrainingDrills)
            .ThenInclude(utd => utd.FixedDrill)
            .Select(ut => new UserTrainingDto()
            {
                Id = ut.TrainingId,
                AgeGroup = ut.Age,
                Drills = ut.UserTrainingDrills.Select(drill => new UserTrainingDrillDto()
                {
                    Duration = drill.Duration,
                    Id = drill.DrillId,
                    Name = drill.FixedDrill.Name,
                    Category = drill.FixedDrill.Category,
                    FixedDrillId = drill.FixedDrill.FixedDrillId
                }).ToList()
            })
            .ToListAsync();
    }
}