using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyMVCApp.Entities.HeroClasses;
using MyMVCApp.Entities.Heroes;
using MyMVCApp.Entities.Skills;
using MyMVCApp.Models;

namespace MyMVCApp.Database;

public class SqlLiteDbContext : DbContext
{
    public DbSet<HeroEntity> Heroes { get; set; }
    public DbSet<ClassEntity> Classes { get; set; }
    public DbSet<SkillEntity> Skills { get; set; }

    public SqlLiteDbContext(DbContextOptions<SqlLiteDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SkillEntity>()
            .HasIndex(s => s.Name)
            .IsUnique();
        
        modelBuilder.Entity<HeroEntity>()
            .HasIndex(h => h.Name)
            .IsUnique();
        
        modelBuilder.Entity<ClassEntity>()
            .HasIndex(c => c.Name)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
    
    public Task<List<HeroEntity>> GetHeroesAsync()
    {
        return Heroes
            .Include(h => h.Class)
            .Include(h => h.Skills).ToListAsync();
    }

    public Task<List<ClassEntity>> GetClassesAsync()
    {
        return Classes
            .Include(c => c.Heroes)
            .ToListAsync();
    }
    
    public Task<List<SkillEntity>> GetSkillsAsync()
    {
        return Skills
            .Include(s => s.Heroes)
            .ToListAsync();
    }
}