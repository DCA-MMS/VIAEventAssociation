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
    public static EventCapacityError IsLessThanOne() => new EventCapacityError(ErrorCode.EventCapacityIsLessThanOne, "The capacity cannot be less than 1");
    
    /// <summary>
    /// Error for when a event capacity is greater than 1000
    /// </summary>
    /// <returns></returns>
    public static EventCapacityError IsGreaterThanAThousand() => new EventCapacityError(ErrorCode.EventCapacityIsGreaterThanAThousand, "The capacity cannot be greater than 1000");


}