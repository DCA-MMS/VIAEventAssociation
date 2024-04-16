using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Common;

namespace VIAEventAssociation.Infrastructure.EfcDmPersistence;

public class EfcUnitOfWork(DbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }
}