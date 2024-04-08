namespace Tests.Fakes.CommandHandlers;

public class FakeServiceProvider : IServiceProvider
{
    public object? GetService(Type serviceType)
    {
        throw new NotImplementedException();
    }
}