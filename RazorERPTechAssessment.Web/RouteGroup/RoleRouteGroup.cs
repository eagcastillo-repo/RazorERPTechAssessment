using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.DapperDB.Entities;

namespace RazorERPTechAssessment.Web.RouteGroup;

public static class RoleRouteGroup
{
    public static RouteGroupBuilder MapRoleApi(this RouteGroupBuilder group)
    {
        group.MapGet("", (IAppReadService<Role> roleService) =>
        {
            return GetAllAsync(roleService);
        });

        group.MapGet("{id}", (IAppReadService<Role> roleService, int id) =>
        {
            return GetByIdAsync(roleService, id);
        });

        return group;
    }

    private static async Task<IResult> GetAllAsync(IAppReadService<Role> roleService)
    {
        try
        {
            var result = await roleService.GetAllAsync();
            return result.Count() > 0 ? Results.Ok(result) : Results.Empty;
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetByIdAsync(IAppReadService<Role> roleService, int id)
    {
        try
        {
            var result = await roleService.GetByIdAsync(id);
            return result != null ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
