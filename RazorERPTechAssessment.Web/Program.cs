using RazorERPTechAssessment.Web.AppConfig;
using RazorERPTechAssessment.Web.RouteGroup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();
builder.Services.RegisterDBServices(builder);
builder.Services.RegisterRateLimiterServices();
builder.Services.RegisterAuthenticationServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapGroup("/users")
    .MapUserApi()
    .WithTags("User API");

app.Run();
