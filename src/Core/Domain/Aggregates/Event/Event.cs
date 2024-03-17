using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Domain.Common.Contracts;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event;

public class Event
{
    // - Attributes
    public EventId Id { get; private set; }
    public EventTitle Title { get; private set; }
    public EventDescription Description { get; private set; }
    public EventStatus Status { get; private set; }
    public EventVisibility Visibility { get; private set; }
    public EventCapacity Capacity { get; private set; }
    public TimeRange? Duration { get; private set; }
    public List<UserId> Participants { get; }
    public List<Invitation> Invitations { get; }
    
    /// <summary>
    /// Contract for handling system time (Is used for testing purposes)
    /// </summary>
    private ISystemTime _systemTime;
    
    // # Constructor
    private Event()
    {
        // * Set the default values
        Id = new EventId();
        Title = Constants.DefaultEventTitle;
        Description = Constants.DefaultEventDescription;
        Status = Constants.DefaultEventStatus;
        Visibility = Constants.DefaultEventVisibility;
        Capacity = Constants.DefaultEventCapacity;
        Participants = new List<UserId>();
        Invitations = new List<Invitation>();
        
        _systemTime = Constants.DefaultSystemTime;
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="Event"/> class
    /// </summary>
    /// <returns>A <see cref="Result"/> contain either the <see cref="Event"/> or errors</returns>
    public static Event Create() 
    {
        // ! No validation is needed here
        // As the Event can only be modified through the methods provided.
        return new Event();
    }
    
    /// <summary>
    /// Changes the title of the <see cref="Event"/>
    /// </summary>
    /// <param name="title">Title to change to.</param>
    /// <returns>A <see cref="Result"/> representing if the title was changed.</returns>
    public Result ChangeTitle(string title)
    {
        // ? Check if the title is modifiable
        if(Status is EventStatus.Active or EventStatus.Cancelled)
        {
            return Result.Failure(EventTitleError.NotModifiable());
        }
        
        // * Create the title
        var titleResult = EventTitle.Create(title);
        
        // ? Check if the title is valid
        if(titleResult.IsFailure)
        {
            // ! If the title is invalid, return a failure result
            return Result.Failure(titleResult.Errors.ToArray());
        }
        
        // * Set the title
        Title = titleResult;
        
        // * Change the status to Draft if it is not already
        if(Status != EventStatus.Draft)
        {
            Status = EventStatus.Draft;
        }
        
        // * Return a success result
        return Result.Success();
    }
    
    /// <summary>
    /// Changes the description of the <see cref="Event"/>
    /// </summary>
    /// <param name="description">Description to change to.</param>
    /// <returns> A <see cref="Result"/> representing if the description was changed.</returns>
    public Result ChangeDescription(string description)
    {
        // ? Check if the title is modifiable
        if(Status is EventStatus.Active or EventStatus.Cancelled)
        {
            return Result.Failure(EventDescriptionError.NotModifiable());
        }
        
        // * Create the description
        var descriptionResult = EventDescription.Create(description);
        
        // ? Check if the description is valid
        if(descriptionResult.IsFailure)
        {
            // ! If the description is invalid, return a failure result
            return Result.Failure(descriptionResult.Errors.ToArray());
        }
        
        // * Set the description
        Description = descriptionResult;
        
        // * Change the status to Draft if it is not already
        if(Status != EventStatus.Draft)
        {
            Status = EventStatus.Draft;
        }
        
        // * Return a success result
        return Result.Success();
    }
    
    /// <summary>
    /// Changes the capacity of the <see cref="Event"/>
    /// </summary>
    /// <param name="capacity">Capacity to change to.</param>
    /// <returns>A <see cref="Result"/> representing if the description was changed.</returns>
    public Result ChangeCapacity(int capacity)
    {
        // * Create the capacity
        var capacityResult = EventCapacity.Create(capacity);

        if (capacityResult.IsFailure)
        {
            // if the capacity is invalid, return a failure result
            return Result.Failure(capacityResult.Errors.ToArray());
        }

        if (Status is EventStatus.Cancelled)
        {
            return Result.Failure(EventCapacityError.NotModifiable());
        }
        
        if (Status is EventStatus.Active && capacity < Capacity)
        {
            return Result.Failure(EventCapacityError.CantReduceCapacityError());
        }
        
        // * Set the capacity
        Capacity = capacityResult;
        
        // * Return a success result
        return Result.Success();
    }

    /// <summary>
    /// Changes the time range of the <see cref="Event"/>
    /// </summary>
    /// <param name="start">Start date and time of the event</param>
    /// <param name="end">End date and time of the event</param>
    /// <returns></returns>
    public Result ChangeDuration(DateTime start, DateTime end)
    {
        // ? Check if the title is modifiable
        if(Status is EventStatus.Active or EventStatus.Cancelled)
        {
            return Result.Failure(EventDescriptionError.NotModifiable());
        }
        
        // * Create the time range
        var timeRangeResult = TimeRange.Create(start, end);
        
        // ? Check if the time range is valid
        if(timeRangeResult.IsFailure)
        {
            // ! If the time range is invalid, return a failure result
            return Result.Failure(timeRangeResult.Errors.ToArray());
        }
        
        // ? Check if the time range is valid for an event.
        var result = ValidateTimeRange(start, end);
        
        // ! If there are any errors, return a failure result
        if(result.IsFailure)
        {
            return Result.Failure(result.Errors.ToArray());
        }
        
        // * Set the time range
        Duration = timeRangeResult;
        
        // * Change the status to Draft if it is not already
        if(Status != EventStatus.Draft)
        {
            Status = EventStatus.Draft;
        }
        
        // * Return a success result
        return Result.Success();
    }

    public Result MakePublic()
    {
        if(Status is EventStatus.Cancelled)
        {
            return Result.Failure(EventVisibilityError.NotModifiable());
        }
        
        Visibility = EventVisibility.Public;
        Status = EventStatus.Draft;

        return Result.Success();
    }
    
    public Result MakePrivate()
    {
        if(Status is EventStatus.Cancelled or EventStatus.Active)
        {
            return Result.Failure(EventVisibilityError.NotModifiable());
        }
        
        Visibility = EventVisibility.Private;
        Status = EventStatus.Draft;

        return Result.Success();
    }

    public Result MakeReady()
    {
        if (Status is EventStatus.Cancelled)
        {
            return Result.Failure(EventError.CantReadyOrActivateCancelledEvent());
        }
        
        // TODO: Add check for if there is a time range. - MHN 
        
        if (_systemTime.Now > Duration?.Start)
        {
            return Result.Failure(EventError.CantReadyOrActivateEventWithStartTimePriorToNow());
        }
        
        if (Title == "Working Title")
        {
            return Result.Failure(EventError.CantReadyOrActivateWhenTitleIsDefault());
        }
        
        Status = EventStatus.Ready;

        return Result.Success();
    }
    
    public Result Activate()
    {
        if (Status is not EventStatus.Ready)
        {
            var readyResult = MakeReady();

            if (readyResult.IsFailure)
            {
                return readyResult;
            }
        }
        
        Status = EventStatus.Active;
        
        return Result.Success();
    }

    public Result Cancel()
    {
        Status = EventStatus.Cancelled;
        
        return Result.Success();
    }

    public Result AddGuest(UserId userId)
    {
        var errors = new List<Error>();
        
        if (IsFull())
        {
            errors.Add(EventRequestError.RequestToFullEvent());
        }

        if (Status != EventStatus.Active)
        {
            errors.Add(EventRequestError.RequestToEventThatIsNotActive());
        }

        if (Visibility != EventVisibility.Public)
        {
            errors.Add(EventRequestError.RequestToEventThatIsNotPublic());
        }

        if (Participants.Contains(userId))
        {
            errors.Add(EventRequestError.RequestToEventGuestIsAlreadyPartaking());
        }

        if (TimeRange != null && TimeRange.Start < _systemTime.Now)
        {
            errors.Add(EventRequestError.RequestToEventInThePast());
        }
        
        if (errors.Count > 0)
        {
            return Result.Failure(errors.ToArray());
        }
        
        Participants.Add(userId);
        return Result.Success();
    }
    
    public Result RemoveGuest(UserId userId)
    {
        if (TimeRange != null && TimeRange.Start < _systemTime.Now)
        {
            return Result.Failure(EventCancelParticipation.CancelParticipationToEventInThePast());
        }
        
        Participants.Remove(userId);
        return Result.Success();
    }

    public Result InviteGuest(UserId userId)
    {
        var errors = new List<Error>();

        if (IsFull())
        {
            errors.Add(EventInvitationError.InvitationToFullEvent());
        }

        if (Status != EventStatus.Ready && Status != EventStatus.Active)
        {
            errors.Add(EventInvitationError.InvitationToNonReadyOrActiveEvent());
        }

        if (errors.Count > 0)
        {
            return Result.Failure(errors.ToArray());
        }
        
        Invitations.Add(Invitation.Create(userId, InvitationStatus.Pending));
        return Result.Success();
    }

    public Result AcceptInvitation(UserId userId)
    {
        var errors = new List<Error>();
        
        var invitation = Invitations.FirstOrDefault(x => x.GuestId == userId);
        if (invitation == null)
        {
            errors.Add(EventInvitationError.InvitationAcceptToGuestNotInvited());
        }

        if (IsFull())
        {
            errors.Add(EventInvitationError.InvitationAcceptToFullEvent());
        }

        if (Status == EventStatus.Cancelled)
        {
            errors.Add(EventInvitationError.InvitationAcceptToCancelledEvent());
        }
        
        if (Status == EventStatus.Ready)
        {
            errors.Add(EventInvitationError.InvitationAcceptToReadyEvent());
        }
        
        if (errors.Count > 0)
        {
            return Result.Failure(errors.ToArray());
        }

        invitation!.Accept();
        return Result.Success();
    }
    
    public Result DeclineInvitation(UserId userId)
    {
        var errors = new List<Error>();
        
        var invitation = Invitations.FirstOrDefault(x => x.GuestId == userId);
        if (invitation == null)
        {
            errors.Add(EventInvitationError.InvitationDeclineToGuestNotInvited());
        }
        
        if (Status == EventStatus.Cancelled)
        {
            errors.Add(EventInvitationError.InvitationDeclineToCancelledEvent());
        }
        
        if (Status == EventStatus.Ready)
        {
            errors.Add(EventInvitationError.InvitationDeclineToReadyEvent());
        }
        
        if (errors.Count > 0)
        {
            return Result.Failure(errors.ToArray());
        }

        invitation!.Decline();
        return Result.Success();
    }

    private bool IsFull()
    {
        return Participants.Count + Invitations.Count(x => x.Status == InvitationStatus.Accepted) >= Capacity;
    }


    /// <summary>
    /// Validates that the time range is valid for an event.
    /// </summary>
    /// <param name="start">Start of the event</param>
    /// <param name="end">Start of the event</param>
    /// <returns>A <see cref="Result"/> contains errors, if any.</returns>
    private Result ValidateTimeRange(DateTime start, DateTime end)
    {
        var errors = new List<Error>();
        
        // ? Is Start date in the past?
        if(start < _systemTime.Now)
        {
            errors.Add(EventTimeRangeError.StartIsInPast());
            return Result.Failure(errors.ToArray());
        }
        
        // ? Start is before 08:00
        if(start.TimeOfDay < TimeSpan.FromHours(8))
        {
            errors.Add(EventTimeRangeError.StartBeforeEight());
            return Result.Failure(errors.ToArray());
        }
        
        // * Calculate the duration
        var duration = end - start;
        
        // ?  Duration is less than 1 hour
        if(duration < TimeSpan.FromHours(1))
        {
            errors.Add(EventTimeRangeError.DurationLessThanOneHour());
            return Result.Failure(errors.ToArray());
        }
        
        // ? Duration is more than 10 hours
        if (duration > TimeSpan.FromHours(10))
        {
            errors.Add(EventTimeRangeError.DurationIsLongerThanTenHours());
            return Result.Failure(errors.ToArray());
        }
        
        return Result.Success();
    }
    
    /// <summary>
    /// Allows for setting the system time for testing purposes
    /// </summary>
    /// <param name="systemTime"></param>
    protected internal void SetSystemTime(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }
    
}
