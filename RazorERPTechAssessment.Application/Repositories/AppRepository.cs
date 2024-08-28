using Dapper;
using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.DapperDB.Context;
using System.Data;

namespace RazorERPTechAssessment.Application.Repositories;

public class AppRepository<T> : IAppRepository<T> where T : class
{
    private readonly DapperContext _context;
    private const CommandType _commandType = CommandType.Text;

    public AppRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync<U>(string sqlStatement, U parameters)
    {
        using (var _connection = _context.CreateConnection())
        {
            return await _connection.ExecuteAsync(sqlStatement, parameters) > 0;
        }
    }

    public async Task<bool> DeleteAsync<U>(string sqlStatement, U parameters)
    {
        using (var _connection = _context.CreateConnection())
        {
            return await _connection.ExecuteAsync(sqlStatement, parameters) > 0;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(string sqlStatement)
    {
        using (var _connection = _context.CreateConnection())
        {
            return await _connection.QueryAsync<T>(sqlStatement, _commandType);
        };
    }

    public async Task<T> FindAsync<U>(string sqlStatement, U parameters)
    {
        using (var _connection = _context.CreateConnection())
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(sqlStatement, parameters);
        };
    }

    public async Task<bool> UpdateAsync<U>(string sqlStatement, U parameters)
    {
        using (var _connection = _context.CreateConnection())
        {
            return await _connection.ExecuteAsync(sqlStatement, parameters) > 0;
        }
    }
}
