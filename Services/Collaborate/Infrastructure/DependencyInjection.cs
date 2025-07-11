using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Infrastructure.Persistence;
using AidManager.Collaborate.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AidManager.Collaborate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure DbContext
        services.AddDbContext<CollaborateDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("CollaborateConnection"), // Ensure this connection string name exists in appsettings.json
                ServerVersion.AutoDetect(configuration.GetConnectionString("CollaborateConnection"))
            ));

        // Register Repositories
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IFavoritePostRepository, FavoritePostRepository>();
        services.AddScoped<ILikedPostRepository, LikedPostRepository>();
        services.AddScoped<IPostRepository, PostRepository>();

        // If you had other services specific to infrastructure (e.g., file storage, external API clients),
        // they would be registered here as well.

        return services;
    }
}
