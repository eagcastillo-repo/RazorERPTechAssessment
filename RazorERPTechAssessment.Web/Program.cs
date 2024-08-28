using Microsoft.IdentityModel.Tokens;
using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.Application.DTO;
using RazorERPTechAssessment.Application.Repositories;
using RazorERPTechAssessment.Application.Services.User;
using RazorERPTechAssessment.DapperDB.Context;
using RazorERPTechAssessment.DapperDB.Entities;
using RazorERPTechAssessment.Web.RouteGroup;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
builder.Services.AddScoped<IAppReadService<User>, UserService>();
builder.Services.AddScoped<IAppCreateService<User, UserUpdateDTO, UserLoginDTO>, UserService>();
//builder.Services.AddScoped<IAppReadService<Company>, CompanyService>();
//builder.Services.AddScoped<IAppReadService<Role>, RoleService>();

builder.Services.AddRateLimiter(options =>
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

builder.Services.AddAuthentication("Bearer")
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

//app.MapGroup("/companies")
//    .MapCompanyApi()
//    .WithTags("Company API");

//app.MapGroup("/roles")
//    .MapRoleApi()
//    .WithTags("Role API");

app.MapGroup("/users")
    .MapUserApi()
    .WithTags("User API");

app.Run();
