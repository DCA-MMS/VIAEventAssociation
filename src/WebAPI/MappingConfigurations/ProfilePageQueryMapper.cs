using VIAEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.MappingConfigurations;

public class ProfilePageQueryMapper : IMappingConfig<ProfilePageRequest, UserProfilePage.Query>
{
    public UserProfilePage.Query Map(ProfilePageRequest input)
    {
        return new UserProfilePage.Query(input.UserId);
    }
}