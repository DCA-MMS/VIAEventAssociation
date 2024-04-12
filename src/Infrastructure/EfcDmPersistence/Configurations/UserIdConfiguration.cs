using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Configurations;

public class UserIdConfiguration : IEntityTypeConfiguration<UserId>
{
    public void Configure(EntityTypeBuilder<UserId> builder)
    {
        // TODO: Find ud af hvorfor FUCK "HasNoKey" skal defineres for UserId, men ikke for EventId (DET GIVER IKKE MENING!!!)
        builder.HasNoKey();
    }
}