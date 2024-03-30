namespace VIAEventAssociation.Core.Domain.Repositories;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
}