using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

/// <summary>
/// Strongly typed Id for the Event aggregate
/// </summary>
public class EventId : Id<Event>
{
    public static Result<EventId> FromString(string id)
    {
        if (Guid.TryParse(id, out var guid))
        {
            return Result<EventId>.Success(new EventId { Value = guid });
        }
        return Result<EventId>.Failure(IdError.InvalidIdConversion());
    }
    // No additional attributes
}