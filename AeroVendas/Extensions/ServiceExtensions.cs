using Contracts;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Entities.ConfigurationModels;

namespace CompanyViewLogsAeroVendass.Extensions;

public static class ServiceExtensions
{
	public static void ConfigureCors(this IServiceCollection services) =>
		services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy", builder =>
			builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()
			.WithExposedHeaders("X-Pagination"));

        });
	
	public static void ConfigureIISIntegration(this IServiceCollection services) =>
		services.Configure<IISOptions>(options =>
		{
			
		});

	public static void ConfigureLoggerService(this IServiceCollection services) =>
		services.AddSingleton<ILoggerManager, LoggerManager>();

	
	public static void ConfigureRepositoryManager(this IServiceCollection services) =>
		services.AddScoped<IRepositoryManager, RepositoryManager>();

	public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration) { 
		services.Configure<EmailConfiguration>(configuration.GetSection("EmailSettings"));;
	}

	public static void ConfigureEmailManager(this IServiceCollection services) =>
	services.AddScoped<IEmailManager, EmailManager>();
	
	public static void ConfigureServiceManager(this IServiceCollection services) =>
		services.AddScoped<IServiceManager, ServiceManager>();

	public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
		services.AddDbContext<RepositoryContext>(opts =>
			opts.UseOracle(configuration.GetConnectionString("OracleConnection")));


    public static void ConfigureIdentity(this IServiceCollection services)
	{
		var builder = services.AddIdentity<User, IdentityRole>(o =>
		{
			o.Password.RequireDigit = true;
			o.Password.RequireLowercase = false;
			o.Password.RequireUppercase = false;
			o.Password.RequireNonAlphanumeric = false;
			o.Password.RequiredLength = 10;
			o.User.RequireUniqueEmail = true;
		})
		.AddEntityFrameworkStores<RepositoryContext>()
		.AddDefaultTokenProviders();
	}
	public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtConfiguration = new JwtConfiguration();
		configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

		var secretKey = Environment.GetEnvironmentVariable("SECRET");

		services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,

				ValidIssuer = jwtConfiguration.ValidIssuer,
				ValidAudience = jwtConfiguration.ValidAudience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
			};
		});
	}

	public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) =>
											services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));
}

