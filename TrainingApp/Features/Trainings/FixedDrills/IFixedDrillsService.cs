namespace TrainingApp.Features.Trainings.FixedDrills;

public interface IFixedDrillsService
{
    Task<List<FixedDrill>> GetFixedDrills();
}