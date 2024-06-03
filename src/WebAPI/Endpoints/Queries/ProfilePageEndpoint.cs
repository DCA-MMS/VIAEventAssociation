using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class ProfilePageEndpoint(IQueryDispatcher queryDispatcher, IMapper mapper) : ApiEndpoint.WithRequest<ProfilePageRequest>.WithResponse<ProfilePageResponse>
{
    [HttpGet("profile/{UserId}")]
    public override async Task<ActionResult<ProfilePageResponse>> HandleAsync([FromRoute] ProfilePageRequest request)
    {
        var query = mapper.Map<UserProfilePage.Query>(request);
        var answer = await queryDispatcher.DispatchAsync(query);
        var response = mapper.Map<ProfilePageResponse>(answer);
        return Ok(response);
    }
}

public record ProfilePageRequest([FromRoute] string UserId);
public record ProfilePageResponse(
    User User,
    int NumberOfUpcomingEvents,
    int NumberOfInvitations,
    List<UpcomingEvents> UpcomingEvents,
    List<PastEvents> PastEvents);

public record User(string FullName, string Email, string? Avatar);

public record UpcomingEvents(string EventId, string Title, int Attendees, string Date, string StartTime);

public record PastEvents(string EventId, string Title);