namespace RazorERPTechAssessment.Application.Abstracts;

public interface IAppCreateService<T, U, V> where T : class where U : class where V : class
{
    Task<bool> CreateAsync(U u);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(T t);
    Task<string> Authenticate(V v);
}
