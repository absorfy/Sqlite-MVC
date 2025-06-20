using MyMVCApp.Entities;
using MyMVCApp.Models;

namespace MyMVCApp.Mappers;

public static class MovieMapper
{
    public static MovieEntity ToEntity(MovieViewModel model)
    {
        return new MovieEntity()
        {
            Title = model.Title,
            Director = model.Director,
            Description = model.Description,
            CreatedAt = DateTime.Now, // або інше джерело часу
            Genres = model.Genres?.Select(name => new GenreEntity
            {
                Name = name,
                CreatedAt = DateTime.Now
            }).ToList() ?? [],
            Sessions = model.Sessions?.Select(date => new SessionEntity
            {
                SessionDate = date,
                CreatedAt = DateTime.Now
            }).ToList() ?? []
        };
    }
    
    public static MovieViewModel ToViewModel(MovieEntity entity)
    {
        return new MovieViewModel(
            entity.Title,
            entity.Director,
            entity.Genres?.Select(g => g.Name).ToList() ?? [],
            entity.Description,
            entity.Sessions?.Select(s => s.SessionDate).ToList() ?? []
        );
    }
}