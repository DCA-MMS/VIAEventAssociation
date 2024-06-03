using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class ChangeCapacityEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<ChangeCapacityRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/change-capacity")]
    public override async Task<ActionResult> HandleAsync(ChangeCapacityRequest request)
    {
        Result<ChangeCapacityCommand> cmdResult = ChangeCapacityCommand.Create(request.Id, request.Capacity);
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

public class ChangeCapacityRequest
{
    [FromRoute] public string Id { get; set; }
    [FromBody] public int Capacity { get; set; }
}