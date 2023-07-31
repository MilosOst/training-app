using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingApp.Features.Trainings.UserTrainingDrills;
using TrainingApp.Features.Users;

namespace TrainingApp.Features.Trainings;

public enum AgeGroup
{
    U10, U11, U12, U13, U14, U15, U16, U17, U18, U19
}

[Table("UserTraining")]
public class UserTraining
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Required]
    public int TrainingId { get; set; }
    
    [Required]
    [Column("AgeGroup")]
    public AgeGroup Age { get; set; }
    
    [Required]
    [Column("Date")]
    public DateOnly Date { get; set; }
    
    [Required]
    public ICollection<UserTrainingDrill> UserTrainingDrills { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required] public User User { get; set; } = null!;

}