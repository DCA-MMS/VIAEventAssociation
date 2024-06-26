﻿using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.AppEntry.Commands.EventCommands;

public class ChangeDescriptionCommand
{
    public EventId Id { get; }
    public EventDescription Description { get; }
    
    // # Constructor
    private ChangeDescriptionCommand(EventId id, EventDescription description)
    {
        Id = id;
        Description = description;
    }
    
    public static Result<ChangeDescriptionCommand> Create(string id, string description)
    {
        // - Convert the string id to EventId
        var eventId = EventId.FromString(id);
        // - Convert the string description to EventDescription
        var eventDescription = EventDescription.Create(description);
        
        List<Error> errors = [];
        
        // ? If both conversions are successful, return a success result with the ChangeDescriptionCommand object
        if (!eventId.IsFailure && !eventDescription.IsFailure)
            return Result<ChangeDescriptionCommand>.Success(new ChangeDescriptionCommand(eventId, eventDescription));
        
        // ! If one or both conversions failed, return a failure result with the errors
        errors.AddRange(eventId.Errors);
        errors.AddRange(eventDescription.Errors);
        return Result<ChangeDescriptionCommand>.Failure(errors.ToArray());
    }
    
    
}