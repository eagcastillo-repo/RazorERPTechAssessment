using RazorERPTechAssessment.Application.DTO;

namespace RazorERPTechAssessment.Application.Abstracts;

public interface IAppReadService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(string userRole);
    Task<T> GetByIdAsync(int id, string userRole);
}
