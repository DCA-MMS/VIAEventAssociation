namespace VIAEventAssociation.Core.Domain.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}