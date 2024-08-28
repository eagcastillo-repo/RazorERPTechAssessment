using RazorERPTechAssessment.Application.Abstracts;

namespace RazorERPTechAssessment.Application.Services;

public abstract class AppService<T> : IAppService<T> where T : class
{
    public readonly IAppRepository<T> _appRepository;

    public AppService(IAppRepository<T> appRepository)
    {
        _appRepository = appRepository;
    }

    public virtual Task<IEnumerable<T>> GetAllAsync(string sqlStatement)
    {
        return _appRepository.GetAllAsync(sqlStatement);
    }
}
