using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class ChangeDurationEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<ChangeDurationRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/change-duration")]
    public override async Task<ActionResult> HandleAsync(ChangeDurationRequest request)
    {
        Result<ChangeDurationCommand> cmdResult = ChangeDurationCommand.Create(request.Id, request.DurationBody.Start, request.DurationBody.End);
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

public class ChangeDurationRequest
{
    [FromRoute] public string Id { get; set; }
    [FromBody] public DurationBody DurationBody { get; set; }
}

public class DurationBody
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}