using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class ActivateEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<ActivateRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/activate")]
    public override async Task<ActionResult> HandleAsync(ActivateRequest request)
    {
        Result<ActivateCommand> cmdResult = ActivateCommand.Create(request.Id);
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

public class ActivateRequest
{
    [FromRoute] public string Id { get; set; }
}