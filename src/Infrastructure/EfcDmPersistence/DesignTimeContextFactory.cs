using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<EfcDbContext>
{
    public EfcDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfcDbContext>();
        optionsBuilder.UseSqlite(@"Data Source=VEADatabase.db");
        return new EfcDbContext(optionsBuilder.Options);
    }
}