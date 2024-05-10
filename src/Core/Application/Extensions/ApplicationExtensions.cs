using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Application.AppEntry.Interfaces;
using Application.Features.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateEventCommand>, CreateEventHandler>();
        services.AddScoped<ICommandHandler<ChangeTitleCommand>, ChangeTitleHandler>();
    }
    
    public static void RegisterDispatcher(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
    }
}