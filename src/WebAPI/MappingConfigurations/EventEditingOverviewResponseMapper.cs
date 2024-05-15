using VIAEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.MappingConfigurations;

public class EventEditingOverviewResponseMapper : IMappingConfig<EventEditingOverview.Answer, EventEditingOverviewResponse>
{
    public EventEditingOverviewResponse Map(EventEditingOverview.Answer input)
    {
        return new EventEditingOverviewResponse(
            Drafts: input.Drafts.Select(e => new Event(e.Id, e.Title)).ToList(),
            Ready: input.Ready.Select(e => new Event(e.Id, e.Title)).ToList(),
            Cancelled: input.Cancelled.Select(e => new Event(e.Id, e.Title)).ToList()
        );
    }
}