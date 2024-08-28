using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.DapperDB.Entities;

namespace RazorERPTechAssessment.Web.RouteGroup;

public static class CompanyRouteGroup
{
    public static RouteGroupBuilder MapCompanyApi(this RouteGroupBuilder group)
    {
        group.MapGet("", (IAppReadService<Company> companyService) =>
        {
            return GetAllAsync(companyService);
        });

        group.MapGet("{id}", (IAppReadService<Company> companyService, int id) =>
        {
            return GetByIdAsync(companyService, id);
        });

        return group;
    }

    private static async Task<IResult> GetAllAsync(IAppReadService<Company> companyService)
    {
        try
        {
            var result = await companyService.GetAllAsync();
            return result.Count() > 0 ? Results.Ok(result) : Results.Empty;
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetByIdAsync(IAppReadService<Company> companyService, int id)
    {
        try
        {
            var result = await companyService.GetByIdAsync(id);
            return result != null ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

}
