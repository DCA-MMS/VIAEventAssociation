using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Application.AppEntry.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tests.Fakes.CommandHandlers.EventHandlers;

namespace Tests.Common.Dispatcher;

[TestFixture]
public class DispatcherInteractionTests
{
    [Test]
    public void Dispatch_With_Zero_Handlers_Registered()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        
        var command = new CreateEventCommand();
        var commandDispatcher = new CommandDispatcher(serviceProvider);
        
        // Act & Assert
        Assert.Throws(typeof(NullReferenceException), () => commandDispatcher.DispatchAsync(command));
    }
    
    [Test]
    public void Dispatch_With_One_Correct_Handler_Registered()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<CreateEventCommand>, FakeCreateEventHandler>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        
        var command = new CreateEventCommand();
        var commandDispatcher = new CommandDispatcher(serviceProvider);
        var fakeHandler = (FakeCreateEventHandler)serviceProvider.GetService<ICommandHandler<CreateEventCommand>>()!;

        // Act
        commandDispatcher.DispatchAsync(command);
        
        // Assert
        Assert.IsTrue(fakeHandler.CommandWasHandled);
    }
    
    [Test]
    public void Dispatch_With_One_Incorrect_Handler_Registered()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<ActivateCommand>, FakeActivateHandler>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        
        var command = new CreateEventCommand();
        var commandDispatcher = new CommandDispatcher(serviceProvider);

        // Act & Assert
        Assert.Throws(typeof(NullReferenceException), () => commandDispatcher.DispatchAsync(command));
    }

    [Test] public void Dispatch_With_Multiple_Including_Correct_Handler_Registered()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<CreateEventCommand>, FakeCreateEventHandler>();
        serviceCollection.AddScoped<ICommandHandler<ActivateCommand>, FakeActivateHandler>();
        serviceCollection.AddScoped<ICommandHandler<ChangeTitleCommand>, FakeChangeTitleHandler>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        
        var command = new CreateEventCommand();
        var commandDispatcher = new CommandDispatcher(serviceProvider);
        var fakeHandler = (FakeCreateEventHandler)serviceProvider.GetService<ICommandHandler<CreateEventCommand>>()!;

        // Act
        commandDispatcher.DispatchAsync(command);
        
        // Assert
        Assert.IsTrue(fakeHandler.CommandWasHandled);
    }
    
    [Test] public void Dispatch_With_Multiple_Excluding_Correct_Handler_Registered()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<ActivateCommand>, FakeActivateHandler>();
        serviceCollection.AddScoped<ICommandHandler<ChangeTitleCommand>, FakeChangeTitleHandler>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        
        var command = new CreateEventCommand();
        var commandDispatcher = new CommandDispatcher(serviceProvider);

        // Act & Assert
        Assert.Throws(typeof(NullReferenceException), () => commandDispatcher.DispatchAsync(command));
    }
    
    [Test] public void Dispatch_With_Multiple_Handlers_But_Only_Correct_Handler_Was_Called()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<CreateEventCommand>, FakeCreateEventHandler>();
        serviceCollection.AddScoped<ICommandHandler<ActivateCommand>, FakeActivateHandler>();
        serviceCollection.AddScoped<ICommandHandler<ChangeTitleCommand>, FakeChangeTitleHandler>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        
        var command = new CreateEventCommand();
        var commandDispatcher = new CommandDispatcher(serviceProvider);
        var fakeCreateEventHandler = (FakeCreateEventHandler)serviceProvider.GetService<ICommandHandler<CreateEventCommand>>()!;
        var fakeActivateHandler = (FakeActivateHandler)serviceProvider.GetService<ICommandHandler<ActivateCommand>>()!;
        var fakeChangeTitleHandler = (FakeChangeTitleHandler)serviceProvider.GetService<ICommandHandler<ChangeTitleCommand>>()!;
        

        // Act
        commandDispatcher.DispatchAsync(command);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsTrue(fakeCreateEventHandler.CommandWasHandled);
            Assert.That(fakeCreateEventHandler.Calls, Is.EqualTo(1));
            Assert.IsFalse(fakeActivateHandler.CommandWasHandled);
            Assert.That(fakeActivateHandler.Calls, Is.EqualTo(0));
            Assert.IsFalse(fakeChangeTitleHandler.CommandWasHandled);
            Assert.That(fakeChangeTitleHandler.Calls, Is.EqualTo(0));
        });
    }
}