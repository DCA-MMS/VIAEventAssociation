using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Core.Domain.Repositories;

public interface IRepository<TAgg>
{
    Task AddAsync(TAgg aggregate);
    Task<TAgg?> GetByIdAsync(Id<TAgg> id);
}