using AidManager.Collaborate.Application; // For AddApplicationServices
using AidManager.Collaborate.Infrastructure; // For AddInfrastructureServices
using AidManager.Collaborate.Infrastructure.Persistence; // For CollaborateDbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 1. Add Application services (like MediatR handlers)
builder.Services.AddApplicationServices();

// 2. Add Infrastructure services (DbContext, repositories)
builder.Services.AddInfrastructureServices(builder.Configuration);

// 3. Add Web API specific services
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Collaborate Service API",
        Version = "v1",
        Description = "API endpoints for the Collaborate microservice."
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Collaborate Service API V1");
        // Optionally, make Swagger UI the default page in development
        // c.RoutePrefix = string.Empty;
    });

    // Apply database migrations automatically in Development environment
    // For Production, a more robust migration strategy is recommended (e.g., during deployment CI/CD pipeline)
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CollaborateDbContext>();
            // dbContext.Database.EnsureCreated(); // Use this if you are not using migrations
            dbContext.Database.Migrate(); // Applies any pending migrations
        }
    }
    catch (Exception ex)
    {
        // Log the error or handle it as needed
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        // Optionally, rethrow or handle to prevent app startup if DB is critical
    }
}

// app.UseHttpsRedirection(); // Enable if HTTPS is configured

app.UseAuthorization();

app.MapControllers();

app.Run();
