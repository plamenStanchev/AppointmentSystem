using AppointmentSystem.Infrastructure.Data;
using AppointmentSystem.Infrastructure.Data.Seed;
using AppointmentSystem.Server.Extensions;
using AppointmentSystem.Server.Features.Appointments.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration)
                .AddIdentity()
                .AddAuthorizationExtension()
                .AddJwtAuthentication(builder.Services.GetApplicationSettings(builder.Configuration))
                .AddApplicationServices()
                .AddSwagger()
                .AddSignalRExtension()
                .AddControllers();

var app = builder.Build();

app.Logger.LogInformation("Server started");


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI();
}

app.AddMapperProfiles();

app.UseHttpsRedirection();

app
    .UseRouting()
    .UseCors(options =>
    {
        options.WithOrigins("http://localhost")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

app
    .UseAuthentication()
    .UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotifyHub>("/notify");
    endpoints.MapControllers();
});

app.ApplyMigrations();

app.Logger.LogInformation("Seeding Started.....");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        new ApplicationDbContextSeeder().SeedAsync(dbContext, services).GetAwaiter().GetResult();
        app.Logger.LogInformation("Seeding Finished");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
    
}
app.Logger.LogInformation("LAUNCHING Server");
app.Run();