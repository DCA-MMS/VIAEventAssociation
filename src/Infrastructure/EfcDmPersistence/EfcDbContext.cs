using Microsoft.EntityFrameworkCore;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence;

public class EfcDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfcDbContext).Assembly);
    }
}