using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Repositories;

public class EfcUserRepository(DbContext dbContext) : EfcRepositoryBase<User>(dbContext), IUserRepository
{
    public override Task<User?> GetByIdAsync(Id<User> id)
    {
        return dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id.Value == id.Value);
    }
}