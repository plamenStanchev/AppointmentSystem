namespace AppointmentSystem.Server.Extensions
{
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using AppointmentSystem.Core.Interfaces.Features;
	using AppointmentSystem.Core.Interfaces.Infrastructure;
	using AppointmentSystem.Core.Interfaces.Repository;
	using AppointmentSystem.Infrastructure.Data;
	using AppointmentSystem.Infrastructure.Data.Identity;
	using AppointmentSystem.Infrastructure.Data.Repositories;
	using AppointmentSystem.Infrastructure.Logging;
	using AppointmentSystem.Infrastructure.Services;
	using AppointmentSystem.Mapper;
	using AppointmentSystem.Server.Features.Appointments;
	using AppointmentSystem.Server.Features.Cities;
	using AppointmentSystem.Server.Features.Department;
	using AppointmentSystem.Server.Features.Doctors;
	using AppointmentSystem.Server.Features.Identity;
	using AppointmentSystem.Server.Features.Patients;
	using AppointmentSystem.Server.Requirements;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.IdentityModel.Tokens;
	using Microsoft.OpenApi.Models;
	using Swashbuckle.AspNetCore.SwaggerGen;

	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSignalExtension(this IServiceCollection services)
		{
			services.AddSignalR();

			return services;
		}

		public static AppSettings GetApplicationSettings(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
			services.Configure<AppSettings>(applicationSettingsConfiguration);
			return applicationSettingsConfiguration.Get<AppSettings>();
		}
		public static IServiceCollection AddDatabase(
			 this IServiceCollection services,
			 IConfiguration configuration)
			=> services
				.AddDbContext<ApplicationDbContext>(options => options
					.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
			=> services
				.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
				.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
				.AddTransient<ICurrentUserService, CurrentUserService>()
				.AddScoped<IAuthorizationHandler, ClaimRequirementsHandler>()
				.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>))
				.AddTransient<IUserService, UserService>()
				.AddTransient<ICityService, CityService>()
				.AddTransient<IDepartmentService, DepartmentService>()
				.AddTransient<IDoctorService, DoctorService>()
				.AddTransient<IPatientService, PatientService>()
				.AddTransient<IAppointmentService, AppointmentService>()
				.AddTransient(a => AutoMapperConfig.MapperInstance);

		public static IServiceCollection AddIdentity(this IServiceCollection services)
		{
			services
				.AddIdentity<ApplicationUser, ApplicationRole>(options =>
				{
					IdentityOptionsProvider.GetIdentityOptions(options);
				}).AddEntityFrameworkStores<ApplicationDbContext>();

			return services;
		}

		public static IServiceCollection AddSwagger(this IServiceCollection services)
			=> services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(
					"v1",
					new OpenApiInfo
					{
						Title = "AppointmentSystem.Server",
						Version = "v1"
					});

				c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
				{
					Type = SecuritySchemeType.Http,
					Scheme = JwtBearerDefaults.AuthenticationScheme,
					In = ParameterLocation.Header,
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = JwtBearerDefaults.AuthenticationScheme
							}
						},
						System.Array.Empty<string>()
					}
				});

				c.DocumentFilter<CustomOrderControllerSwaggerFilter>();

				c.ResolveConflictingActions(apiDescription => apiDescription.First());
			});

		internal class CustomOrderControllerSwaggerFilter : IDocumentFilter
		{
			public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
			{
				var newPaths = new OpenApiPaths();

				swaggerDoc
					.Paths
					.OrderByDescending(x => x.Key.Contains("Identity"))
					.ToList()
					.ForEach(x => newPaths.Add(x.Key, x.Value));

				swaggerDoc.Paths = newPaths;
			}
		}

		public static IServiceCollection AddAuthorizationExtension(this IServiceCollection services)
			=> services.AddAuthorization(options =>
			{

			});

		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
		{
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);

			services
				.AddAuthentication(x =>
				{
					x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false
					};
					x.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							var accessToken = context.Request.Query["access_token"];
							if (string.IsNullOrEmpty(accessToken) is false)
							{
								context.Token = accessToken;
							}

							return Task.CompletedTask;
						}
					};
				});

			return services;
		}
	}
}
