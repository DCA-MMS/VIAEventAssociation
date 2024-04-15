using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                dbValue => UserId.FromGuid(dbValue)
            );
        builder.OwnsOne<FullName>("FullName", x =>
        {
            x.Property(fn => fn.FirstName).HasColumnName("FirstName");
            x.Property(fn => fn.LastName).HasColumnName("LastName");
        });
        builder.ComplexProperty<Email>(
            "Email",
            propBuilder =>
            {
                propBuilder.Property(e => e.Value)
                    .HasColumnName("Email");
            }
        );
    }
}