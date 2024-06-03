using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class UpcomingEventsPageEndpoint(IQueryDispatcher dispatcher, IMapper mapper) : ApiEndpoint.WithRequest<UpcomingEventsPageRequest>.WithResponse<UpcomingEventsPageResponse>
{
    [HttpPost("upcoming-events")]
    public override async Task<ActionResult<UpcomingEventsPageResponse>> HandleAsync([FromBody] UpcomingEventsPageRequest request)
    {
        var query = mapper.Map<UpcomingEventsPage.Query>(request);
        var answer = await dispatcher.DispatchAsync(query);
        var response = mapper.Map<UpcomingEventsPageResponse>(answer);
        return Ok(response);
    }
}

public record UpcomingEventsPageRequest(int EventOffset, int EventLimit);

public record UpcomingEventsPageResponse(List<UpcomingEvent> UpcomingEvents, int CurrentPage, int TotalPages);

public record UpcomingEvent(
    string Title,
    string Description,
    string Date,
    string StartTime,
    int Attendees,
    int MaxAttendees,
    string Visibility);