using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class InviteGuestEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<InviteGuestRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/invite-guest")]
    public override async Task<ActionResult> HandleAsync(InviteGuestRequest request)
    {
        Result<InviteGuestCommand> cmdResult = InviteGuestCommand.Create(request.Id, request.UserId);
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

public class InviteGuestRequest
{
    [FromRoute] public string Id { get; set; }
    [FromBody] public string UserId { get; set; }
}