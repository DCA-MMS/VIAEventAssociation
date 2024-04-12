using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                dbValue => EventId.FromGuid(dbValue)
            );
        builder.ComplexProperty<EventTitle>(
            "Title",
            propBuilder =>
            {
                propBuilder.Property(et => et.Value).HasColumnName("Title");
            }
        );
        builder.ComplexProperty<EventDescription>(
            "Description",
            propBuilder =>
            {
                propBuilder.Property(ed => ed.Value).HasColumnName("Description");
            }
        );
        builder.ComplexProperty<EventCapacity>(
            "Capacity",
            propBuilder =>
            {
                propBuilder.Property(ec => ec.Value).HasColumnName("Capacity");
            }
        );
        
    }
}