using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Interfaces;

public interface ICommandHandler<in TCommand>
{
    Task<Result> HandleAsync(TCommand command);
}