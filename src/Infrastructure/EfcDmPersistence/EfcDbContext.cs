using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Users;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence;

public class EfcDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Event> Events => Set<Event>();
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfcDbContext).Assembly);
    }
}