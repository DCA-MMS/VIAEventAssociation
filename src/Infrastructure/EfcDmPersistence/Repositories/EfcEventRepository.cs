using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Repositories;

public class EfcEventRepository(DbContext dbContext) : EfcRepositoryBase<Event>(dbContext), IEventRepository
{
    private readonly DbContext _dbContext = dbContext;

    public override async Task<Event?> GetByIdAsync(Id<Event> id)
    {
        var list = await _dbContext.Set<Event>()
            .Include(e => e.Invitations)
            .Include(e => e.Participants)
            .Where(e => e.Id.Value == id.Value)
            .ToListAsync();
        return list.First();
    }
}