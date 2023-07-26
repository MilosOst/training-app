using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingApp.Features.Trainings.UserTrainingDrills;

namespace TrainingApp.Features.Trainings.Fixed_drills;


    [Table("FixedDrills")]
public class FixedDrill
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Required]
    public int FixedDrillId { get; set; }
    
    [Column("name"), Required]
    public string Name { get; set; } = string.Empty;
    
    [Column("category"), Required]
    public string Category { get; set; } = String.Empty;
    
    public ICollection<UserTrainingDrill> UserTrainingDrills { get; set; }
}