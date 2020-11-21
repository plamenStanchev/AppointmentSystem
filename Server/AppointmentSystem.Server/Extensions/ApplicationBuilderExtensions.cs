namespace AppointmentSystem.Server.Extensions
{
    using AppointmentSystem.Infrastructure.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
           => app
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                   options.SwaggerEndpoint("/swagger/v1/swagger.json", "My AppointmentSystem Api");
                   options.RoutePrefix = string.Empty;
               });
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
