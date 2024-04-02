using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Dispatcher;

public interface ICommandDispatcher
{
    Task<Result> DispatchAsync<TCommand>(TCommand command);
}