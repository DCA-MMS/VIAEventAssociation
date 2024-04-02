using Application.AppEntry.Dispatcher;
using Logger;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Decorators;

public class LogDecorator(ICommandDispatcher next) : ICommandDispatcher
{
    public async Task<Result> DispatchAsync<TCommand>(TCommand command)
    {
        Result result = await next.DispatchAsync(command);
        
        FileLogger logger = new FileLogger("VEA.log");
        await logger.LogAsync(DateTime.Now, nameof(command), result.IsFailure ? "Failed" : "Success");

        return result;
    }
}