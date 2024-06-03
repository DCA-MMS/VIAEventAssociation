using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class CreateEventEndpoint (ICommandDispatcher dispatcher) : ApiEndpoint.WithoutRequest.WithResponse<CreateEventResponse>
{
    [HttpPost("events/create")]
    public override async Task<ActionResult<CreateEventResponse>> HandleAsync()
    {
        CreateEventCommand cmd = CreateEventCommand.Create();
        Result result = await dispatcher.DispatchAsync(cmd);
        return !result.IsFailure 
            ? Ok(new CreateEventResponse(cmd.Id.Value.ToString())) 
            : BadRequest(result.Errors);
    }
}

public record CreateEventResponse(string Id);