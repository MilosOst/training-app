using Microsoft.EntityFrameworkCore;
using TrainingApp.Data;

namespace TrainingApp.Features.Trainings.FixedDrills;

public class FixedDrillsService: IFixedDrillsService
{
    protected readonly DataContext _db;
    
    public FixedDrillsService(DataContext db)
    {
        _db = db;
    }

    public async Task<List<FixedDrill>> GetFixedDrills()
    {
        return await _db.FixedDrills.ToListAsync();
    }
}