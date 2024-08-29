namespace RazorERPTechAssessment.Application.Abstracts;

public interface IAppRepository<T> where T : class
{
    Task<IEnumerable<T>> ExecuteReadAsync<U>(string sqlStatement, U parameters);
    Task<bool> ExecuteUpdateAsync<U>(string sqlStatement, U parameters);
}
