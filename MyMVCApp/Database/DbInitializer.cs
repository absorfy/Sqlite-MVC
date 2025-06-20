using MyMVCApp.Entities;

namespace MyMVCApp.Database;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if(context.Movies.Any() || context.Genres.Any() || context.Sessions.Any())
            return;
        
        var movie = new MovieEntity
        {
            Title = "The Matrix",
            Director = "Wachowskis",
            Description = "A computer hacker learns the true nature of reality.",
            CreatedAt = DateTime.Now,
            Genres = new List<GenreEntity>
            {
                new GenreEntity { Name = "Action", CreatedAt = DateTime.Now },
                new GenreEntity { Name = "Sci-Fi", CreatedAt = DateTime.Now }
            },
            Sessions = new List<SessionEntity>
            {
                new SessionEntity { SessionDate = DateTime.Today.AddDays(1), CreatedAt = DateTime.Now },
                new SessionEntity { SessionDate = DateTime.Today.AddDays(2), CreatedAt = DateTime.Now }
            }
        };

        context.Movies.Add(movie);
        context.SaveChanges();
    }
}