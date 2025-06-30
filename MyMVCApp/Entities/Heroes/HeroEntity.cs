using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyMVCApp.Entities.HeroClasses;
using MyMVCApp.Entities.Skills;

namespace MyMVCApp.Entities.Heroes;

public class HeroEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 2, ErrorMessage = "Model must be between 2 and 50 characters.")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public int ClassId { get; set; }
    
    [ForeignKey("ClassId")]
    public ClassEntity? Class { get; set; }
    
    public ICollection<SkillEntity> Skills { get; set; } = new List<SkillEntity>();
    
}