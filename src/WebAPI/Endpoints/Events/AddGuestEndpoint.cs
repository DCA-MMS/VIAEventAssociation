using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class AddGuestEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<AddGuestRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/add-guest")]
    public override async Task<ActionResult> HandleAsync(AddGuestRequest request)
    {
        Result<AddGuestCommand> cmdResult = AddGuestCommand.Create(request.Id, request.UserId);
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

public class AddGuestRequest
{
    [FromRoute] public string Id { get; set; }
    [FromBody] public string UserId { get; set; }
}