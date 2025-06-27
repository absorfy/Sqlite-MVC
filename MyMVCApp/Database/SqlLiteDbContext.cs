using Microsoft.EntityFrameworkCore;
using MyMVCApp.Entities;
using MyMVCApp.Entities.Heros;

namespace MyMVCApp.Database;

public class SqlLiteDbContext : DbContext
{
    public DbSet<HeroEntity> Heroes { get; set; }
    public DbSet<AbstractClassEntity> Classes { get; set; }

    public SqlLiteDbContext(DbContextOptions<SqlLiteDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbstractClassEntity>()
            .HasDiscriminator<string>("ClassType")
            .HasValue<WarriorClass>("Warrior")
            .HasValue<MageClass>("Mage");
        
    }
}