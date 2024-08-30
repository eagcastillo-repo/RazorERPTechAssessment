using Dapper;
using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.DapperDB.Context;

namespace RazorERPTechAssessment.Application.Repositories;

public class AppRepository<T> : IAppRepository<T> where T : class
{
    private readonly DapperContext _context;

    public AppRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> ExecuteReadAsync<U>(string sqlStatement, U parameter)
    {
        using (var _connection = _context.CreateConnection())
        {
            return await _connection.QueryAsync<T>(sqlStatement, parameter);
        };
    }

    public async Task<bool> ExecuteUpdateAsync<U>(string sqlStatement, U parameters)
    {
        using (var _connection = _context.CreateConnection())
        {
            return await _connection.ExecuteAsync(sqlStatement, parameters) > 0;
        }
    }
}
