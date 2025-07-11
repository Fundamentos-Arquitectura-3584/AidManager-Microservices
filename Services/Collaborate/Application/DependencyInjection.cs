using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection; // Required for Assembly.GetExecutingAssembly() if used, or typeof().Assembly

namespace AidManager.Collaborate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // services.AddMediatR(Assembly.GetExecutingAssembly()); // Alternative way for MediatR 11+
        services.AddMediatR(typeof(DependencyInjection).Assembly); // Registers all handlers and pre/post processors in this assembly
        return services;
    }
}
