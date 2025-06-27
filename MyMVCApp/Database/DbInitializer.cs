using MyMVCApp.Entities;
using MyMVCApp.Entities.Heros;

namespace MyMVCApp.Database;

public static class DbInitializer
{
    public static void Seed(SqlLiteDbContext context)
    {
        SeedHeros(context);
    }

    public static void SeedHeros(SqlLiteDbContext context)
    {
        if (context.Heroes.Any() || context.Classes.Any())
            return;
        
        context.Classes.AddRange(new MageClass(), new WarriorClass());
        context.SaveChanges();
    }
}