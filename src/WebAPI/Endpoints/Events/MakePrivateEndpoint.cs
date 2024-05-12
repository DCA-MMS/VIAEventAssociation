using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class MakePrivateEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<MakePrivateRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/make-private")]
    public override async Task<ActionResult> HandleAsync(MakePrivateRequest request)
    {
        Result<MakePrivateCommand> cmdResult = MakePrivateCommand.Create(request.Id);
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

public class MakePrivateRequest
{
    [FromRoute] public string Id { get; set; }
}