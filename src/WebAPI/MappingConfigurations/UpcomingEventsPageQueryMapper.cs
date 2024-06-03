using VIAEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.MappingConfigurations;

public class UpcomingEventsPageQueryMapper : IMappingConfig<UpcomingEventsPageRequest, UpcomingEventsPage.Query>
{
    public UpcomingEventsPage.Query Map(UpcomingEventsPageRequest input)
    {
        return new UpcomingEventsPage.Query(input.EventOffset, input.EventLimit);
    }
}