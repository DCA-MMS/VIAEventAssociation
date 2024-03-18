namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventCapacityError : Error
{
    /// <inheritdoc cref="Error"/>
    public override ErrorCode Code { get; init; }
    
    /// <inheritdoc cref="Error"/>
    public override string? Message { get; init; }
    
    private EventCapacityError(ErrorCode code, string message) : base(code, message) { }
    
    /// <summary>
    /// Error for when a event capacity is negative
    /// </summary>
    /// <returns></returns>
    public static EventCapacityError IsNegative() => new EventCapacityError(ErrorCode.EventCapacityIsNegative, "The capacity cannot be negative");
    
    /// <summary>
    /// Error for when a event capacity is less than 1
    /// </summary>
    /// <returns></returns>
    public static EventCapacityError IsLessThanFive() => new EventCapacityError(ErrorCode.EventCapacityIsLessThanOne, "The capacity cannot be less than 5");
    
    /// <summary>
    /// Error for when a event capacity is greater than 1000
    /// </summary>
    /// <returns></returns>
    public static EventCapacityError IsGreaterThanFifty() => new EventCapacityError(ErrorCode.EventCapacityIsGreaterThanAThousand, "The capacity cannot be greater than 50");
    
    /// <summary>
    /// Error for unmodifiable event capacity
    /// </summary>
    public static EventCapacityError NotModifiable() => new EventCapacityError(ErrorCode.EventCapacityNotModifiable, "The capacity cannot be modified");
    
    /// <summary>
    /// Error for when attempting to reduce the capacity of an active event
    /// </summary>
    public static EventCapacityError CantReduceCapacityError() => new EventCapacityError(ErrorCode.EventCapacityCantBeReduced, "The capacity cannot be reduced for an active event");


}