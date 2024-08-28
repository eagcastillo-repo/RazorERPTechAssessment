using Dapper;
using RazorERPTechAssessment.Application.Abstracts;

namespace RazorERPTechAssessment.Application.Services.Role;

public class RoleService : IAppReadService<DapperDB.Entities.Role>
{
    public readonly IAppRepository<DapperDB.Entities.Role> _appRepository;

    public RoleService(IAppRepository<DapperDB.Entities.Role> appRepository)
    {
        _appRepository = appRepository;
    }
    public async Task<IEnumerable<DapperDB.Entities.Role>> GetAllAsync()
    {
        var sqlStatement = "SELECT * FROM Role";
        var companies = await _appRepository.GetAllAsync(sqlStatement);
        return companies;
    }

    public async Task<DapperDB.Entities.Role> GetByIdAsync(int id)
    {
        var sqlStatement = "SELECT * FROM Role WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        return await _appRepository.FindAsync(sqlStatement, parameters);
    }
}
