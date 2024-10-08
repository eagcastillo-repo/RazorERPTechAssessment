﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.Application.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RazorERPTechAssessment.Application.Services.User;

public class UserService : IAppReadService<DapperDB.Entities.User>, IAppCreateService<DapperDB.Entities.User, UserUpdateDTO, UserLoginDTO>
{
    public readonly IAppRepository<DapperDB.Entities.User> _appRepository;
    private readonly IConfiguration _config;

    public UserService(IAppRepository<DapperDB.Entities.User> appRepository, IConfiguration config)
    {
        _appRepository = appRepository;
        _config = config;
    }

    public async Task<bool> CreateAsync(UserUpdateDTO createUser)
    {
        var sqlStatement = "INSERT INTO dbo.[User] (Name, Role, Company, Password) VALUES (@Name, @Role, @Company, @Password)";

        var parameters = new DynamicParameters();
        parameters.Add("Name", createUser.Name);
        parameters.Add("Role", createUser.Role);
        parameters.Add("Company", createUser.Company);
        parameters.Add("Password", createUser.Password);

        return await _appRepository.ExecuteUpdateAsync(sqlStatement, parameters);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sqlStatement = "DELETE FROM dbo.[User] WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        return await _appRepository.ExecuteUpdateAsync(sqlStatement, parameters);
    }

    public async Task<IEnumerable<DapperDB.Entities.User>> GetAllAsync(string userRole)
    {
        var sqlStatement = "SELECT * FROM dbo.[User]";
        var parameters = new DynamicParameters();

        if (userRole.Equals("User", StringComparison.OrdinalIgnoreCase))
        {
            sqlStatement = sqlStatement + " WHERE Role = @Role";
            parameters.Add("Role", 1);
        }

        return await _appRepository.ExecuteReadAsync(sqlStatement, parameters);
    }

    public async Task<DapperDB.Entities.User?> GetByIdAsync(int id, string userRole)
    {
        var sqlStatement = "SELECT * FROM dbo.[User] WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        if (userRole.Equals("User", StringComparison.OrdinalIgnoreCase))
        {
            sqlStatement = sqlStatement + " AND Role = @Role";
            parameters.Add("Role", 1);
        }

        var user = await _appRepository.ExecuteReadAsync(sqlStatement, parameters);

        return user.FirstOrDefault();
    }

    public async Task<bool> UpdateAsync(DapperDB.Entities.User user)
    {
        var sqlStatement = "UPDATE dbo.[User] SET Name = @Name, Role = @Role, Company = @Company, Password = @Password WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", user.Id);
        parameters.Add("Name", user.Name);
        parameters.Add("Role", user.Role);
        parameters.Add("Company", user.Company);
        parameters.Add("Password", user.Password);

        return await _appRepository.ExecuteUpdateAsync(sqlStatement, parameters);
    }

    public async Task<string> Authenticate(UserLoginDTO user)
    {
        var sqlStatement = "SELECT * FROM dbo.[User] WHERE Name = @Name AND Password = @Password";
        var parameters = new DynamicParameters();
        parameters.Add("Name", user.Name);
        parameters.Add("Password", user.Password);

        var authorizedUser = await _appRepository.ExecuteReadAsync(sqlStatement, parameters);

        if (authorizedUser.Count() > 0)
        {
            return GenerateToken(authorizedUser.FirstOrDefault());
        }

        return string.Empty;
    }

    private string GenerateToken(DapperDB.Entities.User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Name),
            new Claim(ClaimTypes.Role, ((DapperDB.Enums.Role)user.Role).ToString())
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
