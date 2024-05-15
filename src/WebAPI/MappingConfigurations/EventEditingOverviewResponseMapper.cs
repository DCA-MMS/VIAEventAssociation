using VIAEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.MappingConfigurations;

public class EventEditingOverviewResponseMapper : IMappingConfig<EventEditingOverview.Answer, EventEditingOverviewResponse>
{
    public EventEditingOverviewResponse Map(EventEditingOverview.Answer source)
    {
        return new EventEditingOverviewResponse(
            Drafts: source.Drafts.Select(e => new Event(e.Id, e.Title)).ToList(),
            Ready: source.Ready.Select(e => new Event(e.Id, e.Title)).ToList(),
            Cancelled: source.Cancelled.Select(e => new Event(e.Id, e.Title)).ToList()
        );
    }
}