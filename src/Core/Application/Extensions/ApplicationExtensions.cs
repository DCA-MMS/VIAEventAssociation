using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        //TODO: Register all command handlers
    }
    
    public static void RegisterDispatcher(this IServiceCollection services)
    {
        //TODO: Register dispatcher
    }
}