namespace AssignmentSystem.Server.Extensions
{
    using AssignmentSystem.Core.Interfaces;
    using AssignmentSystem.Core.Interfaces.Repository;
    using AssignmentSystem.Infrastructure.Data;
    using AssignmentSystem.Infrastructure.Data.Identity;
    using AssignmentSystem.Infrastructure.Data.Repositories;
    using AssignmentSystem.Infrastructure.Logging;
    using AssignmentSystem.Infrastructure.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Text;

    public static class ServiceCollectionExtensions
    {
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
            =>
            services
               .AddDbContext<ApplicationDbContext>(options => options
                   .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
           => services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
                      .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
                      .AddTransient<ICurrentUserService, CurrentUserService>()
                      .AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
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
                        Title = "AssignmentSystem.Server",
                        Version = "v1"
                    });
            });

        public static IServiceCollection AddJwtAuthentication(
           this IServiceCollection services,
           AppSettings appSettings)
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
                });

            return services;
        }
    }
}
