using AidManager.Iam.Application;
using AidManager.Iam.Infrastructure;
using AidManager.Iam.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "IAM",
        Version = "v1",
        Description = "IAM Endpoints for AidManager."
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IamDbContext>();
    db.Database.Migrate(); // Applies any pending migrations
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();