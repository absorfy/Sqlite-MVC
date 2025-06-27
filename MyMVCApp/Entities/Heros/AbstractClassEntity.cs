using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMVCApp.Entities.Heros;

public abstract class AbstractClassEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public abstract string Name { get; set; }
    
    public ICollection<HeroEntity> Heroes { get; set; }
    
}