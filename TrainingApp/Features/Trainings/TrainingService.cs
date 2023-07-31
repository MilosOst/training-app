using Microsoft.EntityFrameworkCore;
using TrainingApp.Data;
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
        foreach (var drill in req.DrillInputs)
        { 
            //check validation
            UserTrainingDrill userTrainingDrill = new UserTrainingDrill
            {
                Duration = drill.Duration,
                DrillId = drill.DrillId,
                
            };
            
            userTrainingDrills.Add(userTrainingDrill);
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
        
        // foreach (var drill in newTraining.UserTrainingDrills)
        // {
        //     drill.TrainingId = newTraining.Id;
        // }
        //
        // await _db.SaveChangesAsync();
        
        
    }


}