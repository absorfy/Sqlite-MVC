using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMVCApp.Entities.Heros;

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
    public AbstractClassEntity? Class { get; set; }
}