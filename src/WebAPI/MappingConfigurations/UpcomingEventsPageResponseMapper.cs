using VIAEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.MappingConfigurations;

public class UpcomingEventsPageResponseMapper : IMappingConfig<UpcomingEventsPage.Answer, UpcomingEventsPageResponse>
{
    public UpcomingEventsPageResponse Map(UpcomingEventsPage.Answer input)
    {
        return new UpcomingEventsPageResponse(
            UpcomingEvents: input.Events.Select(e => new UpcomingEvent(
                Title: e.Title,
                Description: e.Description,
                Date: e.Date,
                StartTime: e.StartTime,
                Attendees: e.Attendees,
                MaxAttendees: e.MaxAttendees,
                Visibility: e.Visibility
            )).ToList(),
            CurrentPage: input.CurrentPage,
            TotalPages: input.TotalPages
        );
    }
}