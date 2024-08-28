namespace RazorERPTechAssessment.Application.Abstracts;

public interface IAppCreateService<T, U> where T : class where U : class
{
    Task<bool> CreateAsync(U u);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(T t);

}
