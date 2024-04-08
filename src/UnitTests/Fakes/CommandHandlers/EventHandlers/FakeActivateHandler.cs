using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Tests.Fakes.CommandHandlers.EventHandlers;

public class FakeActivateHandler : ICommandHandler<ActivateCommand>
{
    public bool CommandWasHandled { get; private set; }
    public Task<Result> HandleAsync(ActivateCommand command)
    {
        CommandWasHandled = true;
        return Task.FromResult(Result.Success());
    }
}