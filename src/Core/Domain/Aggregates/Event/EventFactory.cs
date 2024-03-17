using System.Reflection;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Common.Contracts;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event;

public class EventFactory
{
    // - Attributes
    private readonly Event _event = Event.Create();
    private ISystemTime _systemTime = Constants.GetTestSystemTime();
    
    /// <summary>
    /// Initializes the creation of a new event
    /// </summary>
    /// <returns></returns>
    public static EventFactory Create()
    {
        return new EventFactory();
    }
    
    /// <summary>
    /// Adds a title to the event
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public EventFactory WithTitle(string title)
    {
        // Get EventTitle private constructor and instantiate a new EventTitle
        var eventTitleConstructor = typeof(EventTitle)
            .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {typeof(string)}, null);
        if (eventTitleConstructor == null) throw new NullReferenceException("EventTitle constructor not found");
        
        var eventTitle = (EventTitle) eventTitleConstructor.Invoke(new object[] {title});
        
        // Get the Title property of the Event class and set the value
        var titleProperty = typeof(Event).GetProperty("Title");
        if (titleProperty == null) throw new NullReferenceException("Title property not found");
        
        titleProperty.SetValue(_event, eventTitle);
        return this;
    }
    
    /// <summary>
    /// Adds a description to the event
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public EventFactory WithDescription(string description)
    {
        // Get the private constructor of EventDescription and instantiate a new EventDescription
        var eventDescriptionConstructor = typeof(EventDescription)
            .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {typeof(string)}, null);
        if (eventDescriptionConstructor == null) throw new NullReferenceException("EventDescription constructor not found");
        
        var eventDescription = (EventDescription) eventDescriptionConstructor.Invoke(new object[] {description});
        
        // Get the Description property of the Event class and set the value
        var descriptionProperty = typeof(Event).GetProperty("Description");
        if (descriptionProperty == null) throw new NullReferenceException("Description property not found");
        
        descriptionProperty.SetValue(_event, eventDescription);
        return this;
    }
    
    /// <summary>
    /// Adds a capacity to the event
    /// </summary>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public EventFactory WithCapacity(int capacity)
    {
        // Get the private constructor of EventCapacity and instantiate a new EventCapacity
        var eventCapacityConstructor = typeof(EventCapacity)
            .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {typeof(int)}, null);
        if (eventCapacityConstructor == null) throw new NullReferenceException("EventCapacity constructor not found");
        
        var eventCapacity = (EventCapacity) eventCapacityConstructor.Invoke(new object[] {capacity});
        
        // Get the Capacity property of the Event class and set the value
        var titleProperty = typeof(Event).GetProperty("Capacity");
        if (titleProperty == null) throw new NullReferenceException("Capacity property not found");
        
        titleProperty.SetValue(_event, eventCapacity);
        return this;
    }
    
    /// <summary>
    /// Adds a time range to the event
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public EventFactory WithTimeRange(DateTime start, DateTime end)
    {
        // Get the private constructor of TimeRange and instantiate a new TimeRange
        var timeRangeConstructor = typeof(TimeRange)
            .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {typeof(DateTime), typeof(DateTime)}, null);
        if (timeRangeConstructor == null) throw new NullReferenceException("TimeRange constructor not found");
        
        var eventTimeRange = (TimeRange) timeRangeConstructor.Invoke(new object[] {start, end});
        
        // Get the TimeRange property of the Event class and set the value
        var durationProperty = typeof(Event).GetProperty("Duration");
        if (durationProperty == null) throw new NullReferenceException("Duration property not found");
        
        durationProperty.SetValue(_event, eventTimeRange);
        return this;
    }
    
    /// <summary>
    /// Adds a status to the event
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public EventFactory WithStatus(EventStatus status)
    {
        // Get the Status property of the Event class and set the value
        var statusProperty = typeof(Event).GetProperty("Status");
        if (statusProperty == null) throw new NullReferenceException("Status property not found");
        
        statusProperty.SetValue(_event, status);
        return this;
    }
    
    /// <summary>
    /// Adds a visibility to the event
    /// </summary>
    /// <param name="visibility"></param>
    /// <returns></returns>
    public EventFactory WithVisibility(EventVisibility visibility)
    {
        // Get the Visibility property of the Event class and set the value
        var visibilityProperty = typeof(Event).GetProperty("Visibility");
        if (visibilityProperty == null) throw new NullReferenceException("Visibility property not found");
        
        visibilityProperty.SetValue(_event, visibility);
        return this;
    }
    
    public EventFactory WithSystemTime(ISystemTime systemTime)
    {
        _systemTime = systemTime;
        return this;
    }
    
    /// <summary>
    /// Returns the built event
    /// </summary>
    /// <returns></returns>
    public Event Build()
    {
        return _event;
    }

    /// <summary>
    /// Builds a version of the event with the test system time: <see cref="Constants.GetTestSystemTime"/>
    /// </summary>
    /// <returns></returns>
    public Result<Event> BuildTest()
    {
        _event.SetSystemTime(_systemTime);
        return _event;
    }
}