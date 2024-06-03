using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class MakePublicEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<MakePublicRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/make-public")]
    public override async Task<ActionResult> HandleAsync(MakePublicRequest request)
    {
        Result<MakePublicCommand> cmdResult = MakePublicCommand.Create(request.Id);
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

public class MakePublicRequest
{
    [FromRoute] public string Id { get; set; }
}