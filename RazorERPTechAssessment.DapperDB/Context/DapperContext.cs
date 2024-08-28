using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace RazorERPTechAssessment.DapperDB.Context;

public class DapperContext
{
    private readonly IConfiguration _config;
    private const string _connectionString = "Data Source=EGGHEAD;Initial Catalog=RazorERPTechAssessment;Integrated Security=True;Encrypt=False";

    public DapperContext(IConfiguration config)
    {
        _config = config;
    }

    public DbConnection CreateConnection() => new SqlConnection(_connectionString);
}
