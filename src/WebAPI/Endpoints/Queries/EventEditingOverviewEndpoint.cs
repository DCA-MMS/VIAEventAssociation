using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class EventEditingOverviewEndpoint(IQueryDispatcher queryDispatcher, IMapper mapper) : ApiEndpoint.WithoutRequest.WithResponse<EventEditingOverviewResponse>
{
    [HttpGet("events/overview")]
    public override async Task<ActionResult<EventEditingOverviewResponse>> HandleAsync()
    {
        EventEditingOverview.Query query = new(); // Empty query, no mapping is needed
        EventEditingOverview.Answer answer = await queryDispatcher.DispatchAsync(query);
        EventEditingOverviewResponse response = mapper.Map<EventEditingOverviewResponse>(answer);
        return Ok(response);
    }
}

public record EventEditingOverviewResponse(
    List<Event> Drafts,
    List<Event> Ready,
    List<Event> Cancelled);
public record Event(string Id, string Title);