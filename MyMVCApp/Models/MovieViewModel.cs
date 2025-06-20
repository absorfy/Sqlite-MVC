using System.ComponentModel.DataAnnotations;

namespace MyMVCApp.Models;

public class MovieViewModel
{
    [Display(Name="Назва")]
    public string Title { get; set; }
    [Display(Name="Режисер")]
    public string Director { get; set; }
    [Display(Name="Жанри")]
    public List<string> Genres { get; set; }
    [Display(Name="Опис")]
    public string Description { get; set; }
    [Display(Name="Сеанси")]
    public List<DateTime> Sessions { get; set; }
    
    public MovieViewModel(string title, string director, List<string> genres, string description, List<DateTime> sessions)
    {
        Title = title;
        Director = director;
        Genres = genres;
        Description = description;
        Sessions = sessions;
    }
}