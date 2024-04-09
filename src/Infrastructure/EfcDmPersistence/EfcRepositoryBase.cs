using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Domain.Repositories;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence;

public abstract class EfcRepositoryBase<TAgg>(DbContext context) : IRepository<TAgg> where TAgg : class
{
    public virtual async Task AddAsync(TAgg aggregate)
    {
        await context.Set<TAgg>().AddAsync(aggregate);
    }

    public virtual async Task<TAgg?> GetByIdAsync(Id<TAgg> id)
    {
        return await context.Set<TAgg>().FindAsync(id);
    }
}