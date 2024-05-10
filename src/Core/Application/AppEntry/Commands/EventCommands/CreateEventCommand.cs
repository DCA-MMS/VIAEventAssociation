using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Application.AppEntry.Commands.EventCommands;

public class CreateEventCommand
{
    public EventId Id { get; set; }
    // ! Should it contain any properties?
    
    // # Constructor
    public static CreateEventCommand Create()
    {
        return new CreateEventCommand();
    }
}