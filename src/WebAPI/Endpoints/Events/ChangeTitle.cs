using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class ChangeTitle(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<ChangeTitleRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/change-title")]
    public override async Task<ActionResult> HandleAsync(ChangeTitleRequest request)
    {
        Result<ChangeTitleCommand> cmdResult = ChangeTitleCommand.Create(request.Id, request.Title);
        if (cmdResult.IsFailure)
        {
            return BadRequest(cmdResult.Errors);
        }

        Result result = await dispatcher.DispatchAsync(cmdResult.Value);
        return !result.IsFailure
            ? Ok()
            : BadRequest(result.Errors);
    }
}

public class ChangeTitleRequest
{
    [FromRoute] public string Id { get; set; }
    [FromBody] public string Title { get; set; }
}