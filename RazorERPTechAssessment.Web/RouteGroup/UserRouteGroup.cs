using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.Application.DTO;
using RazorERPTechAssessment.DapperDB.Entities;

namespace RazorERPTechAssessment.Web.RouteGroup;

public static class UserRouteGroup
{
    public static RouteGroupBuilder MapUserApi(this RouteGroupBuilder group)
    {
        group.MapGet("", (IAppReadService<User> userService) =>
        {
            return GetAllAsync(userService);
        });

        group.MapGet("{id}", (IAppReadService<User> userService, int id) =>
        {
            return GetByIdAsync(userService, id);
        });

        group.MapPost("/create", (IAppCreateService<User, UserUpdateDTO> userCreateService, UserUpdateDTO user) =>
        {
            return CreateAsync(userCreateService, user);
        });

        group.MapPatch("/update", (IAppCreateService<User, UserUpdateDTO> userCreateService, User user) =>
        {
            return UpdateAsync(userCreateService, user);
        });

        group.MapDelete("/delete/{id}", (IAppCreateService<User, UserUpdateDTO> userCreateService, int id) =>
        {
            return DeleteAsync(userCreateService, id);
        });

        group.MapPost("/authenticate", (IAppAuthorizeService<UserLoginDTO> userAuthorizeService, UserLoginDTO user) =>
        {
            return Authenticate(userAuthorizeService, user);
        });

        return group;
    }

    private static async Task<IResult> GetAllAsync(IAppReadService<User> userService)
    {
        try
        {
            var result = await userService.GetAllAsync();
            return result.Count() > 0 ? Results.Ok(result) : Results.Empty;
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetByIdAsync(IAppReadService<User> userService, int id)
    {
        try
        {
            var result = await userService.GetByIdAsync(id);
            return result != null ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> CreateAsync(IAppCreateService<User, UserUpdateDTO> userCreateService, UserUpdateDTO user)
    {
        try
        {
            var result = await userCreateService.CreateAsync(user);
            return result == true ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateAsync(IAppCreateService<User, UserUpdateDTO> userCreateService, User user)
    {
        try
        {
            var result = await userCreateService.UpdateAsync(user);
            return result == true ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteAsync(IAppCreateService<User, UserUpdateDTO> userCreateService, int id)
    {
        try
        {
            var result = await userCreateService.DeleteAsync(id);
            return result == true ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> Authenticate(IAppAuthorizeService<UserLoginDTO> userAuthorizeService, UserLoginDTO user)
    {
        try
        {
            var result = await userAuthorizeService.Authenticate(user);
            return !string.IsNullOrEmpty(result) ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
