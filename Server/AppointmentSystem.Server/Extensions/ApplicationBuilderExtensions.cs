namespace AppointmentSystem.Server.Extensions
{
	using System.Reflection;
	using AppointmentSystem.Infrastructure.Data;
	using AppointmentSystem.Mapper;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using Swashbuckle.AspNetCore.SwaggerUI;

	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
		   => app
			   .UseSwagger()
			   .UseSwaggerUI(options =>
			   {
				   options.SwaggerEndpoint("/swagger/v1/swagger.json", Assembly.GetExecutingAssembly().GetName().Name);
				   options.RoutePrefix = string.Empty;
				   options.DocExpansion(DocExpansion.None);
			   });

		public static void ApplyMigrations(this IApplicationBuilder app)
		{
			using var services = app.ApplicationServices.CreateScope();

			var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();

			dbContext.Database.Migrate();
		}

		public static void AddMapperProfiles(this IApplicationBuilder _)
			=> AutoMapperConfig.RegisterMappings(Assembly.GetAssembly(typeof(Startup)));
	}
}
