using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class AcceptInvitationEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<AcceptInvitationRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/accept-invitation")]
    public override async Task<ActionResult> HandleAsync(AcceptInvitationRequest request)
    {
        Result<AcceptInvitationCommand> cmdResult = AcceptInvitationCommand.Create(request.Id, request.UserId);
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

public class AcceptInvitationRequest
{
    [FromRoute] public string Id { get; set; }
    [FromBody] public string UserId { get; set; }
}