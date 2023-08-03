using TrainingApp.Features.Trainings.UserTrainingDrills;
using TrainingApp.Migrations;
using Category = TrainingApp.Features.Trainings.FixedDrills.Category;

namespace TrainingApp.Features.Trainings.Models;

public class UserTrainingDto
{
    public int Id { get; set; }
    public AgeGroup AgeGroup { get; set; }
    public List<UserTrainingDrillDto> Drills { get; set; }
}

public class UserTrainingDrillDto
{
    public int Id { get; set; }
    public int Duration { get; set; }
    public string Name { get; set; } = string.Empty;
    public Category Category { get; set; }
}