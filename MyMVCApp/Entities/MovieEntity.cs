using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMVCApp.Entities;

[Table("MovieEntities")]
public class MovieEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    [Column("Title", TypeName = "nvarchar(50)")]
    public string Title { get; set; }
    
    [Required]
    [StringLength(50)]
    [Column("Director", TypeName = "nvarchar(50)")]
    public string Director { get; set; }
    
    public List<GenreEntity> Genres { get; set; }
    
    [Required]
    [StringLength(500)]
    [Column("Description", TypeName = "nvarchar(500)")]
    public string Description { get; set; }
    
    public List<SessionEntity> Sessions { get; set; }
    
    [Required]
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }
}