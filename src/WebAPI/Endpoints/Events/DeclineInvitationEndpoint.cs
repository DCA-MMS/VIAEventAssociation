using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class DeclineInvitationEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<DeclineInvitationRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/decline-invitation")]
    public override async Task<ActionResult> HandleAsync(DeclineInvitationRequest request)
    {
        Result<DeclineInvitationCommand> cmdResult = DeclineInvitationCommand.Create(request.Id, request.UserId);
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

public class DeclineInvitationRequest
{
    [FromRoute] public string Id { get; set; }
    [FromBody] public string UserId { get; set; }
}