using VIAEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.MappingConfigurations;

public class ProfilePageResponseMapper : IMappingConfig<UserProfilePage.Answer, ProfilePageResponse>
{
    public ProfilePageResponse Map(UserProfilePage.Answer input)
    {
        return new ProfilePageResponse(
            new User(input.User.FullName, input.User.Email, input.User.Avatar),
            input.NumberOfUpcomingEvents,
            input.NumberOfInvitations,
            input.UpcomingEvents.Select(x => new UpcomingEvents(x.EventId, x.Title, x.Attendees, x.Date, x.StartTime)).ToList(),
            input.PastEvents.Select(x => new PastEvents(x.EventId, x.Title)).ToList());
    }
}