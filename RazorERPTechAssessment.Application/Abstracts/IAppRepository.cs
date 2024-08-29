namespace RazorERPTechAssessment.Application.Abstracts;

public interface IAppRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync<U>(string sqlStatement, U parameters);
    Task<T> FindAsync<U>(string sqlStatement, U parameters);
    Task<bool> ExecuteUpdateAsync<U>(string sqlStatement, U parameters);
}
