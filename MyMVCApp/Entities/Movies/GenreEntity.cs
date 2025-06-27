using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMVCApp.Entities;

[Table("GenreEntities")]
public class GenreEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    [Column("Name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    
    [Required]
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
    public int MovieId { get; set; }
    public MovieEntity Movie { get; set; }
}