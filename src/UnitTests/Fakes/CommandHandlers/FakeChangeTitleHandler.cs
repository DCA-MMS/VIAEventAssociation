using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Tests.Fakes.CommandHandlers.EventHandlers;

public class FakeChangeTitleHandler : ICommandHandler<ChangeTitleCommand>
{
    public bool CommandWasHandled { get; private set; }
    public int Calls { get; private set; }
    public Task<Result> HandleAsync(ChangeTitleCommand command)
    {
        CommandWasHandled = true;
        Calls++;
        return Task.FromResult(Result.Success());
    }
}