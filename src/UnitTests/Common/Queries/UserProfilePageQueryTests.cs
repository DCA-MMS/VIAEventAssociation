﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Tests.Common.DatabaseTest;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Core.QueryContracts.QueryDispatching;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;
using VIAEventAssociation.Infrastructure.EfcQueries.Queries;
using VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

namespace Tests.Common.Queries;

public class UserProfilePageQueryTests
{
    [Test]
    public void PersonalProfilePage_Query_Json()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        
 
        // #1: Add the DbContext
        serviceCollection.AddScoped<VeadatabaseContext>(_ => SetUpReadContext().Seed());
        
        // #2: Add the QueryHandler.
        serviceCollection.AddScoped<IQueryHandler<UserProfilePage.Query,UserProfilePage.Answer>,  ProfilePageQueryHandler>();
        
        // #3: Build the service provider
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        
        // Act - Dispatch the query
        var query = new UserProfilePage.Query("86a87d88-93bf-404f-a5b0-5d60a5cf19b7");
        QueryDispatcher queryDispatcher = new(serviceProvider);
        var answer = queryDispatcher.DispatchAsync(query);
        
        // Assert
        
        // #4: Print the result (with Pretty Print)
        Console.WriteLine(JsonConvert.SerializeObject(answer.Result, Formatting.Indented));
    }

    private static VeadatabaseContext SetUpReadContext()
    {
        var testDbName = "Test.db";
        DbContextOptionsBuilder<VeadatabaseContext> optionsBuilder = new();
        optionsBuilder.UseSqlite($"Data Source = {testDbName}");
        VeadatabaseContext context = new(optionsBuilder.Options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}