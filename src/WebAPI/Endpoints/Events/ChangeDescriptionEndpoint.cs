﻿using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class ChangeDescriptionEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<ChangeDescriptionRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/change-description")]
    public override async Task<ActionResult> HandleAsync(ChangeDescriptionRequest request)
    {
        Result<ChangeDescriptionCommand> cmdResult = ChangeDescriptionCommand.Create(request.Id, request.Description);
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
public class ChangeDescriptionRequest
{
    [FromRoute] public string Id { get; set; }
    [FromBody] public string Description { get; set; }
}