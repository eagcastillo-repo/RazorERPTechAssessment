using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace RazorERPTechAssessment.DapperDB.Context;

public class DapperContext
{
    private readonly IDbConnection _connection;

    public DapperContext(IDbConnection connection)
    {
        _connection = connection;
    }

    public DbConnection CreateConnection() => new SqlConnection(_connection.ConnectionString);
}
