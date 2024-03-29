namespace AppointmentSystem.Server
{
	using AppointmentSystem.Server.Extensions;
	using AppointmentSystem.Server.Features.Appointments.Hubs;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;

	public class Startup
	{
		public Startup(IConfiguration configuration)
			=> this.Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
			=> services
				.AddDatabase(this.Configuration)
				.AddIdentity()
				.AddAuthorizationExtension()
				.AddJwtAuthentication(services.GetApplicationSettings(this.Configuration))
				.AddApplicationServices()
				.AddSwagger()
				.AddSignalRExtension()
				.AddControllers();

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
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
		}
	}
}
