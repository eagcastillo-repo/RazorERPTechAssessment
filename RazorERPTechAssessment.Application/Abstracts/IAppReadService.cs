using RazorERPTechAssessment.Application.DTO;

namespace RazorERPTechAssessment.Application.Abstracts;

public interface IAppReadService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
}
