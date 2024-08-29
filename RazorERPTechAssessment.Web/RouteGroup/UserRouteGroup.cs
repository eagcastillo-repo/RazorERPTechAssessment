using Microsoft.AspNetCore.Authorization;
using RazorERPTechAssessment.Application.Abstracts;
using RazorERPTechAssessment.Application.DTO;
using RazorERPTechAssessment.DapperDB.Entities;
using System.Security.Claims;

namespace RazorERPTechAssessment.Web.RouteGroup;

public static class UserRouteGroup
{
    
    public static RouteGroupBuilder MapUserApi(this RouteGroupBuilder group)
    {
        group.MapGet("", [Authorize] (IAppReadService<User> userService, ClaimsPrincipal user) =>
        {
            return GetAllAsync(userService, user);
        });

        group.MapGet("{id}", [Authorize] (IAppReadService<User> userService, int id, ClaimsPrincipal user) =>
        {
            return GetByIdAsync(userService, id, user);
        });

        group.MapPost("/create", [Authorize(Roles = "Admin")] (IAppCreateService<User, UserUpdateDTO, UserLoginDTO> userCreateService, UserUpdateDTO user) =>
        {
            return CreateAsync(userCreateService, user);
        });

        group.MapPatch("/update", [Authorize(Roles = "Admin")] (IAppCreateService<User, UserUpdateDTO, UserLoginDTO> userCreateService, User user) =>
        {
            return UpdateAsync(userCreateService, user);
        });

        group.MapDelete("/delete/{id}", [Authorize(Roles = "Admin")] (IAppCreateService<User, UserUpdateDTO, UserLoginDTO> userCreateService, int id) =>
        {
            return DeleteAsync(userCreateService, id);
        });

        group.MapPost("/authenticate", (IAppCreateService<User, UserUpdateDTO, UserLoginDTO> userCreateService, UserLoginDTO user) =>
        {
            return Authenticate(userCreateService, user);
        }).AllowAnonymous();

        return group;
    }

    private static async Task<IResult> GetAllAsync(IAppReadService<User> userService, ClaimsPrincipal user)
    {
        try
        {
            var userRole = user.Claims.ToList()[1].Value;
            var result = await userService.GetAllAsync(userRole);
            return result.Count() > 0 ? Results.Ok(result) : Results.Empty;
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetByIdAsync(IAppReadService<User> userService, int id, ClaimsPrincipal user)
    {
        try
        {
            var userRole = user.Claims.ToList()[1].Value;
            var result = await userService.GetByIdAsync(id, userRole);
            return result != null ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> CreateAsync(IAppCreateService<User, UserUpdateDTO, UserLoginDTO> userCreateService, UserUpdateDTO user)
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

    private static async Task<IResult> UpdateAsync(IAppCreateService<User, UserUpdateDTO, UserLoginDTO> userCreateService, User user)
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

    private static async Task<IResult> DeleteAsync(IAppCreateService<User, UserUpdateDTO, UserLoginDTO> userCreateService, int id)
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

    private static async Task<IResult> Authenticate(IAppCreateService<User, UserUpdateDTO, UserLoginDTO> userCreateService, UserLoginDTO user)
    {
        try
        {
            var result = await userCreateService.Authenticate(user);
            return !string.IsNullOrEmpty(result) ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
