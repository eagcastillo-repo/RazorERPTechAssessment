using Dapper;
using RazorERPTechAssessment.Application.Abstracts;

namespace RazorERPTechAssessment.Application.Services.Company;

public class CompanyService : IAppReadService<DapperDB.Entities.Company>
{
    public readonly IAppRepository<DapperDB.Entities.Company> _appRepository;

    public CompanyService(IAppRepository<DapperDB.Entities.Company> appRepository)
    {
        _appRepository = appRepository;
    }
    public async Task<IEnumerable<DapperDB.Entities.Company>> GetAllAsync()
    {
        var sqlStatement = "SELECT * FROM Company";
        var companies =  await _appRepository.GetAllAsync(sqlStatement);
        return companies;
    }

    public async Task<DapperDB.Entities.Company> GetByIdAsync(int id)
    {
        var sqlStatement = "SELECT * FROM Company WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        return await _appRepository.FindAsync(sqlStatement, parameters);
    }
}
