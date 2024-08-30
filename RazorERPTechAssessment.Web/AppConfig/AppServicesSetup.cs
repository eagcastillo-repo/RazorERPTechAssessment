using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.Application.DTO;
using RazorERPTechAssessment.Application.Repositories;
using RazorERPTechAssessment.Application.Services.User;
using RazorERPTechAssessment.DapperDB.Context;
using RazorERPTechAssessment.DapperDB.Entities;
using System.Data;
using System.Text;
using System.Threading.RateLimiting;

namespace RazorERPTechAssessment.Web.AppConfig
{
    public static class AppServicesSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
            services.AddScoped<IAppReadService<User>, UserService>();
            services.AddScoped<IAppCreateService<User, UserUpdateDTO, UserLoginDTO>, UserService>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection RegisterDBServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration.GetConnectionString("Demo");
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));
            services.AddSingleton<DapperContext>();

            return services;
        }

        public static IServiceCollection RegisterRateLimiterServices(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = 429;
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 10,
                            QueueLimit = 0,
                            Window = TimeSpan.FromMinutes(1)
                        }));
            });

            return services;
        }

        public static IServiceCollection RegisterAuthenticationServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization();
            return services;
        }
    }
}
