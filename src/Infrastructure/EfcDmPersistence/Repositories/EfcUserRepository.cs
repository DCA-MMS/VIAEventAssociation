using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Repositories;

public class EfcUserRepository(EfcDbContext dbContext) : EfcRepositoryBase<User>(dbContext), IUserRepository
{

    public override async Task<User?> GetByIdAsync(Id<User> id)
    {
        return await dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == id);
    }
}