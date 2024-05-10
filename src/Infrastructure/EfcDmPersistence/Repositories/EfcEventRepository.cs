using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Repositories;

public class EfcEventRepository(DbContext dbContext) : EfcRepositoryBase<Event>(dbContext), IEventRepository
{
    public override Task<Event?> GetByIdAsync(Id<Event> id)
    {
        return dbContext.Set<Event>()
            .Include(e => e.Invitations)
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id.Value == id.Value);
    }
}