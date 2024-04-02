using VIAEventAssociation.Core.Domain.Common;

namespace Tests.Fakes;

public class FakeUnitOfWork : IUnitOfWork
{
    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}