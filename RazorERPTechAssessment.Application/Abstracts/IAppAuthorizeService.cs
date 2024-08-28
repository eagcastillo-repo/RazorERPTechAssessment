namespace RazorERPTechAssessment.Application.Abstracts;

public interface IAppAuthorizeService<T> where T : class
{
    Task<string> Authenticate(T t);
}
