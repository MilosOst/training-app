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

    public async Task<List<SendTraining>> TrainingHistory(CreateDateRequest req, string userId)
    {

        var list = await _db.UserTrainings
            .Where(p => p.UserId.ToString() == userId &&
                        p.Date == req.ScheduledDate)
            .OrderBy(p=>p.Date)
            .Take(10)
            .Include(p=>p.UserTrainingDrills)
            .Select(p=>new SendTraining
            {
                Age = p.Age,
                Date = p.Date,
                UserTrainingDrills = p.UserTrainingDrills
            })
            .ToListAsync();
        
        return list;
        

    }
    public async Task UpdateTraining(CreateTrainingRequest req, string userId, int TrainingID)
    {
        var user = await _db.UserTrainings
                .Include(x => x.UserTrainingDrills)
            .FirstOrDefaultAsync(p => p.TrainingId == TrainingID);


        if (user == null)
        {
            throw new NotFoundException("This does not exist.");
        } 

        if (user.UserId.ToString() != userId)
        {
            throw new ForbiddenException("You do not have access to do this.");
        }
        
        List<UserTrainingDrill> newUserTrainingDrills = new List<UserTrainingDrill>();
        Dictionary<string, List<String>> validationErrors = new Dictionary<string, List<string>>();

        for (int i = 0; i < req.Drills.Count; i++)
        {
            var drill = req.Drills[i];
            if (!_db.FixedDrills.Any(p => p.FixedDrillId == drill.DrillId))
            {
                validationErrors[$"Drills[{i}].DrillId"] = new List<string>() { "Invalid DrillId" };
            }

            newUserTrainingDrills.Add(new UserTrainingDrill()
            {
                Duration = drill.Duration,
                DrillId = drill.DrillId
            });
        }
        
        if (validationErrors.Count > 0)
        {
            throw new BadRequestException(validationErrors);
        }
        
        _db.UserTrainingDrills.RemoveRange(user.UserTrainingDrills);
        user.UserTrainingDrills.Clear();
        
        user.Age = req.Age;
        user.Date = req.ScheduledDate;
        user.UserTrainingDrills = newUserTrainingDrills;
        
        await _db.SaveChangesAsync();
        
    }

    public async Task DeleteTraining(string userId, int TrainingID)
    {
        var training = await _db.UserTrainings
            .Include((Ut => Ut.UserTrainingDrills))
            .FirstOrDefaultAsync(p => p.TrainingId == TrainingID);

        if (training == null)
        {
            throw new NotFoundException("This does not exist.");
        }

        if (training.UserId.ToString() != userId)
        {
            throw new ForbiddenException("You do not have access to do this.");
        }


        _db.UserTrainingDrills.RemoveRange(training.UserTrainingDrills);

        _db.UserTrainings.Remove(training);
        
        await _db.SaveChangesAsync();
        
        
    }
}