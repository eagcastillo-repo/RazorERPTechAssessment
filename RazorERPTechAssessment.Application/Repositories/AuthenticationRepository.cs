using Dapper;
using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.DapperDB.Context;
using System.Data;

namespace RazorERPTechAssessment.Application.Repositories;

public class AuthenticationRepository<T> : IAuthenticationRepository<T> where T : class
{
    private readonly DapperContext _context;
    private const CommandType _commandType = CommandType.Text;

    public AuthenticationRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<T> AuthenticateAsync<U>(string sqlStatement, U parameters)
    {
        using (var _connection = _context.CreateConnection())
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(sqlStatement, parameters);
        };
    }
}
