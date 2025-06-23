using Microsoft.EntityFrameworkCore;
using MyMVCApp.Database;
using MyMVCApp.Entities;
using MyMVCApp.Mappers;
using MyMVCApp.Models;

namespace MyMVCApp.Repositories;

public class MovieRepository
{
    public static MovieViewModel? GetMovieByTitle(SqlLiteDbContext context, string title)
    {
        var movieEntity = context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Sessions)
            .FirstOrDefault(m => m.Title == title);

        return movieEntity == null ? null : MovieMapper.ToViewModel(movieEntity);
    }

    public static List<MovieViewModel> GetAllMovies(SqlLiteDbContext context)
    {
        return context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Sessions)
            .Select(MovieMapper.ToViewModel).ToList();
    }
}