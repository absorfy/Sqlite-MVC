using Microsoft.EntityFrameworkCore;
using MyMVCApp.Entities;

namespace MyMVCApp.Database;

public class SqlLiteDbContext : DbContext
{
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<CarEntity> Cars { get; set; }

    public SqlLiteDbContext(DbContextOptions<SqlLiteDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenreEntity>()
            .HasOne(g => g.Movie)
            .WithMany(g => g.Genres)
            .HasForeignKey(g => g.MovieId);
        
        modelBuilder.Entity<SessionEntity>()
            .HasOne(s => s.Movie)
            .WithMany(s => s.Sessions)
            .HasForeignKey(s => s.MovieId);
    }
}