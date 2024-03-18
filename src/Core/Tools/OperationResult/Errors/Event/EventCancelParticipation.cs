namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventCancelParticipation : Error
{
    public override ErrorCode Code { get; init; }
    
    public override string? Message { get; init; }
    
    private EventCancelParticipation(ErrorCode code, string message) : base(code, message) { }
    
    public static EventCancelParticipation CancelParticipationToEventInThePast() => new (ErrorCode.CancelParticipationToEventInThePast, "You cannot cancel your participation for past or ongoing events");
}