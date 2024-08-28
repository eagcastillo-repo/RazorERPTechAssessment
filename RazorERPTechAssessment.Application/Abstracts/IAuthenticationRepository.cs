namespace RazorERPTechAssessment.Application.Abstracts;

public interface IAuthenticationRepository<T> where T : class
{
    Task<T> AuthenticateAsync<U>(string sqlStatement, U parameters);
}
