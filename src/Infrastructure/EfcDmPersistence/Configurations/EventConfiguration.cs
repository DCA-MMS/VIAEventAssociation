using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIAEventAssociation.Core.Domain.Aggregates.Event;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    private IEntityTypeConfiguration<Event> _entityTypeConfigurationImplementation;
    
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        _entityTypeConfigurationImplementation.Configure(builder);
    }
}