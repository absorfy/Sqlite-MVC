using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMVCApp.Entities;

[Table("SessionEntities")]
public class SessionEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }
    
    [Required]
    [Column("SessionDate")]
    public DateTime SessionDate { get; set; }
    
    [Required]
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
    public int MovieId { get; set; }
    public MovieEntity Movie { get; set; }
}