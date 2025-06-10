using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Tasks.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}
