using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tests.Fakes.CommandHandlers.EventHandlers;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Core.QueryContracts.QueryDispatching;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;
using VIAEventAssociation.Infrastructure.EfcQueries.Queries;

namespace Tests.Common.Queries;

public class UserProfilePageQueryTests
{
    [Test]
    public void PersonalProfilePage_Query_Json()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IQueryHandler<UserProfilePage.Query,UserProfilePage.Answer>, ProfilePageQueryHandler>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        
        var query = new UserProfilePage.Query("86a87d88-93bf-404f-a5b0-5d60a5cf19b7");
        QueryDispatcher queryDispatcher = new(serviceProvider);
        var answer = queryDispatcher.DispatchAsync(query);
        
        Console.WriteLine(answer.Result);
    }
    
    
}