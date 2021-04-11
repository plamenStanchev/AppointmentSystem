namespace AppointmentSystem.Server.Extensions
{
    using AppointmentSystem.Infrastructure.Data;
    using AppointmentSystem.Mapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using System.Reflection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
           => app
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                   options.SwaggerEndpoint("/swagger/v1/swagger.json", "AppointmentSystemApi");
                   options.RoutePrefix = string.Empty;
                   options.DocExpansion(DocExpansion.None);
               });

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }

        public static void AddMapperProfiles(this IApplicationBuilder app)
            => AutoMapperConfig.RegisterMappings(typeof(Startup).GetTypeInfo().Assembly);
    }
}
