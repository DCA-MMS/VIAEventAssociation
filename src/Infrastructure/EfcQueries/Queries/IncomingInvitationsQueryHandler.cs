using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Queries;

public class IncomingInvitationsQueryHandler(VeadatabaseContext context) : IQueryHandler<IncomingInvitations.Query, IncomingInvitations.Answer>
{
    public async Task<IncomingInvitations.Answer> HandleAsync(IncomingInvitations.Query query)
    {
        // #1: Validate the query
        var correctIdResult = UserId.FromString(query.UserId);
        
        // ? If the id is invalid, throw an exception
        if (correctIdResult.IsFailure)
        {
            throw new Exception("Invalid user id format");
        }
        
        // #1a: Extract the id
        var userId = correctIdResult.Value.Value.ToString();
        
        // #2: Query the database (Include the Offset and Limit)
        var invitations = await context.Invitations
            .Where(i => i.GuestId == userId)
            .Skip(query.Offset)
            .Take(query.Limit)
            .Select(i => new IncomingInvitations.Invitation(
                i.EventId,
                i.Event.Title,
                DateTime.Parse(i.Event.DurationStart).ToString("yyyy-MM-dd HH:mm"),
                i.Event.Participants.Count,
                i.Event.Capacity
            ))
            .ToListAsync();
        
        return new IncomingInvitations.Answer(invitations);
    }
}