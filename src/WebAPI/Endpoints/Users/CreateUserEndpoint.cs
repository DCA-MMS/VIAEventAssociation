﻿using Application.AppEntry.Commands.UserCommands;
using Application.AppEntry.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Users;

public class CreateUserEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<CreateUserRequest>.WithResponse<CreateUserResponse>
{
    [HttpPost("users/create")]
    public override async Task<ActionResult<CreateUserResponse>> HandleAsync(CreateUserRequest request)
    {
        Result<CreateUserCommand> cmdResult = CreateUserCommand.Create(request.UserBody.FirstName, request.UserBody.LastName, request.UserBody.Email);
        if (cmdResult.IsFailure)
        {
            return BadRequest(cmdResult.Errors);
        }
        
        Result result = await dispatcher.DispatchAsync(cmdResult.Value);
        return !result.IsFailure 
            ? Ok(new CreateUserResponse(cmdResult.Value.Id.Value.ToString())) 
            : BadRequest(result.Errors);
    }
}

public class CreateUserRequest
{
    [FromBody] public UserBody UserBody { get; set; }
}

public class UserBody
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
public record CreateUserResponse(string Id);