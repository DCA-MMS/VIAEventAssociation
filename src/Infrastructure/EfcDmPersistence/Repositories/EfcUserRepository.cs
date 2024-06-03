using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Repositories;

public class EfcUserRepository(EfcDbContext dbContext) : EfcRepositoryBase<User>(dbContext), IUserRepository
{
    
}