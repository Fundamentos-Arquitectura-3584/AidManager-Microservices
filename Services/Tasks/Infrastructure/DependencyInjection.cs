using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Application.Interfaces;
using Tasks.Infrastructure.Repositories;

namespace Tasks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TasksDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 21)))); // Adjust server version as needed

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();

            return services;
        }
    }
}
