using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingApp.Features.Trainings.UserTrainingDrills;
using TrainingApp.Features.Users;

namespace TrainingApp.Features.Trainings;


    [Table("UserTraining")]
public class UserTraining
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Required]
    public int TrainingId { get; set; }
    
    [Required]
    [Column("AgeGroup")]
    public string Age { get; set; } = String.Empty;
    
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