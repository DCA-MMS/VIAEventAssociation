using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence.Repositories;

public class EfcEventRepository(DbContext dbContext) : EfcRepositoryBase<Event>(dbContext), IEventRepository
{
    private readonly DbContext _dbContext = dbContext;

    public override async Task<Event?> GetByIdAsync(Id<Event> id)
    {
        var @event = await _dbContext.Set<Event>()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (@event != null)
        {
            _dbContext.Entry(@event).Collection(x => x.Invitations).Load();
            _dbContext.Entry(@event).Collection(x => x.Participants).Load();
        }

        return @event;
    }
}