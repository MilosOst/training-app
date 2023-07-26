using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingApp.Features.Trainings.Fixed_drills;

namespace TrainingApp.Features.Trainings.UserTrainingDrills;

    [Table("UserTrainingDrills")]
public class UserTrainingDrill
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Required]
    public int UserTrainingDrillId { get; set; }
    
    // maybe change maxvalue to one hour
    [Range(30, int.MaxValue)]
    [Column("duration"), Required]
    public int Duration { get; set; }
    
    [Required]
    [ForeignKey("FixedDrill")]
    public int DrillId { get; set; }
    
    [Required]
    public FixedDrill FixedDrill { get; set; } = null!;

    [Required]
    [ForeignKey("UserTraining")]
    public int UserTrainingId { get; set; }
    
    [Required]
    public UserTraining UserTraining { get; set; } = null!;
    
}