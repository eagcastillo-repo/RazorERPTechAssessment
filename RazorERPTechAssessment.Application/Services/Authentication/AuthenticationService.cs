using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.Application.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RazorERPTechAssessment.Application.Services.Authentication;

public class AuthenticationService : IAppAuthorizeService<UserLoginDTO>
{
    public readonly IAuthenticationRepository<UserLoginDTO> _appRepository;
    private readonly IConfiguration _config;

    public AuthenticationService(IAuthenticationRepository<UserLoginDTO> appRepository, IConfiguration config)
    {
        _appRepository = appRepository;
        _config = config;
    }

    public async Task<string> Authenticate(UserLoginDTO user)
    {
        var sqlStatement = "SELECT * FROM dbo.[User] WHERE Name = @Name AND Password = @Password";
        var parameters = new DynamicParameters();
        parameters.Add("Name", user.Name);
        parameters.Add("Password", user.Password);

        var authorizedUser = await _appRepository.AuthenticateAsync(sqlStatement, parameters);

        if (authorizedUser != null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        return string.Empty;
    }
}
