using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;

public class InvitationId : Id<Invitation>
{
    public static InvitationId FromGuid(Guid guid) => new() { Value = guid };
}