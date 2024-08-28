namespace RazorERPTechAssessment.Application.Abstracts;

public interface IAppRepository<T> where T : class
{
    Task<bool> CreateAsync<U>(string sqlStatement, U parameters);
    Task<bool> DeleteAsync<U>(string sqlStatement, U parameters);
    Task<IEnumerable<T>> GetAllAsync(string sqlStatement);
    Task<T> FindAsync<U>(string sqlStatement, U parameters);
    Task<bool> UpdateAsync<U>(string sqlStatement, U parameters);
}
