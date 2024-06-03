using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Repositories;

public class EfcEventRepository(DbContext dbContext) : EfcRepositoryBase<Event>(dbContext), IEventRepository
{
    
}