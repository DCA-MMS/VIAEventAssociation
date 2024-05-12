using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Commands.UserCommands;
using Application.AppEntry.Dispatcher;
using Application.AppEntry.Interfaces;
using Application.Features.EventHandlers;
using Application.Features.UserHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateEventCommand>, CreateEventHandler>();
        services.AddScoped<ICommandHandler<ChangeTitleCommand>, ChangeTitleHandler>();
        services.AddScoped<ICommandHandler<ChangeDescriptionCommand>, ChangeDescriptionHandler>();
        services.AddScoped<ICommandHandler<ChangeDurationCommand>, ChangeDurationHandler>();
        services.AddScoped<ICommandHandler<ChangeCapacityCommand>, ChangeCapacityHandler>();
        services.AddScoped<ICommandHandler<MakePublicCommand>, MakePublicHandler>();
        services.AddScoped<ICommandHandler<MakePrivateCommand>, MakePrivateHandler>();
        services.AddScoped<ICommandHandler<MakeReadyCommand>, MakeReadyHandler>();
        services.AddScoped<ICommandHandler<ActivateCommand>, ActivateHandler>();
        services.AddScoped<ICommandHandler<AddGuestCommand>, AddGuestHandler>();
        services.AddScoped<ICommandHandler<RemoveGuestCommand>, RemoveGuestHandler>();
        services.AddScoped<ICommandHandler<InviteGuestCommand>, InviteGuestHandler>();
        services.AddScoped<ICommandHandler<AcceptInvitationCommand>, AcceptInvitationHandler>();
        services.AddScoped<ICommandHandler<DeclineInvitationCommand>, DeclineInvitationHandler>();
        services.AddScoped<ICommandHandler<CreateUserCommand>, CreateUserHandler>();
    }
    
    public static void RegisterDispatcher(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
    }
}