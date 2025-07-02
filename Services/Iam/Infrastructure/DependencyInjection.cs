using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AidManager.Iam.Infrastructure.Persistence;
using Iam.Application.Contracts; // For IUserRepository and ICompanyRepository
using Iam.Infrastructure.Repositories; // For UserRepository and CompanyRepository

namespace AidManager.Iam.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IamDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
                ));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }
}