using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class ViewSingleEventEndpoint(IQueryDispatcher dispatcher, IMapper mapper) : ApiEndpoint.WithRequest<ViewSingleEventRequest>.WithResponse<ViewSingleEventResponse>
{
    [HttpGet("events/{EventId}")]
    public override async Task<ActionResult<ViewSingleEventResponse>> HandleAsync([FromRoute] ViewSingleEventRequest request)
    {
        var query = mapper.Map<ViewSingleEvent.Query>(request);
        var answer = await dispatcher.DispatchAsync(query);
        var response = mapper.Map<ViewSingleEventResponse>(answer);
        return Ok(response);
    }
}

public record ViewSingleEventRequest([FromRoute] string EventId);

public record ViewSingleEventResponse(EventInfo EventInfo, List<Guest> Guests);

public record EventInfo(
    string Id,
    string Title,
    string Description,
    string Location,
    string Date,
    string Visibility,
    int NumberOfParticipants,
    int MaxParticipants);

public record Guest(string Avatar, string FullName);