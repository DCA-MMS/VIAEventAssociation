using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class MakeReadyEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<MakeReadyRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/make-ready")]
    public override async Task<ActionResult> HandleAsync(MakeReadyRequest request)
    {
        Result<MakeReadyCommand> cmdResult = MakeReadyCommand.Create(request.Id);
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

public class MakeReadyRequest
{
    [FromRoute] public string Id { get; set; }
}