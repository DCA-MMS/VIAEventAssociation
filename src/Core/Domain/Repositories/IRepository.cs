using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Core.Domain.Repositories;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(Id<T> id);
}