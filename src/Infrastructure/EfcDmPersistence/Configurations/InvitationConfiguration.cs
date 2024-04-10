using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Configurations;

public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .HasConversion(
                id => id.Value,
                dbValue => InvitationId.FromGuid(dbValue)
            );
    }
}