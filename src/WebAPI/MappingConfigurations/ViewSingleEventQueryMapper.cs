using VIAEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.MappingConfigurations;

public class ViewSingleEventQueryMapper : IMappingConfig<ViewSingleEventRequest, ViewSingleEvent.Query>
{
    public ViewSingleEvent.Query Map(ViewSingleEventRequest input)
    {
        return new(input.EventId);
    }
}