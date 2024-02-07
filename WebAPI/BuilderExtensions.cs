using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace WebAPI
{
    public static class BuilderExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>()
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<TaskDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureLogger(this ILoggingBuilder logging, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            logging.ClearProviders();

            logging.AddSerilog(logger);
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IGenerateTokenService, GenerateTokenService>();

            services.AddTransient<IProjectService, ProjectService>();

            services.AddTransient<ITaskService, TaskService>();

            services.AddTransient<ITokenService, TokenService>();

            services.AddTransient<IUserProjectService, UserProjectService>();
        }

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var emailOptionsConfiguration = configuration.GetSection("Email");

            services.Configure<EmailOptions>(emailOptionsConfiguration);

            var tokenOptionsConfiguration = configuration.GetSection("Token");

            services.Configure<BuisnessLogicLayer.Models.TokenOptions>(tokenOptionsConfiguration);
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("Token").Get<BuisnessLogicLayer.Models.TokenOptions>();

            services.AddAuthentication(t =>
            {
                t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(t =>
            {
                t.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Secret))
                };
            });
        }
    }
}
