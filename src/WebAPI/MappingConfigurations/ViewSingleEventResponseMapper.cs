using VIAEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.MappingConfigurations;

public class ViewSingleEventResponseMapper : IMappingConfig<ViewSingleEvent.Answer, ViewSingleEventResponse>
{
    public ViewSingleEventResponse Map(ViewSingleEvent.Answer input)
    {
        return new ViewSingleEventResponse(
            EventInfo: new EventInfo(
                Id: input.EventInfo.Id,
                Title: input.EventInfo.Title,
                Description: input.EventInfo.Description,
                Location: input.EventInfo.Location,
                Date: input.EventInfo.Date,
                Visibility: input.EventInfo.Visibility,
                NumberOfParticipants: input.EventInfo.NumberOfParticipants,
                MaxParticipants: input.EventInfo.MaxParticipants
            ),
            Guests: input.Guests.Select(g => new Guest(
                Avatar: g.Avatar,
                FullName: g.FullName
            )).ToList());
    }
}