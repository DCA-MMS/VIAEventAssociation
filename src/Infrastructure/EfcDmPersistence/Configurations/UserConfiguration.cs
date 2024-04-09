using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIAEventAssociation.Core.Domain.Aggregates.Users;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private IEntityTypeConfiguration<User> _entityTypeConfigurationImplementation;
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        _entityTypeConfigurationImplementation.Configure(builder);
    }
}