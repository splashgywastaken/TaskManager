using System.Data.Entity.Core;
using System.Security.Claims;
using TaskManager.Service.Entities.User;

namespace TaskManager.Service.Validation;

public static class UserValidation
{
    /// <summary>
    /// Method to check if userId is the same as the userId held in context
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userId"></param>
    /// <param name="userService"></param>
    /// <returns>Returns result of userId == userId(which is held by user in context)</returns>
    public static async Task<bool> CheckUserIdentity(HttpContext context, int userId, IUserService userService)
    {
        var emailInContext = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (emailInContext == null)
        {
            throw new ObjectNotFoundException("claim not found");
        }

        var user = await userService.GetById(userId);
        if (user == null)
        {
            throw new ObjectNotFoundException("user not found");
        }

        return user.UserEmail == emailInContext.Value;
    }

    /// <summary>
    /// Return role for user held in context
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string? GetUserRole(HttpContext context)
    {
        var roleInContext = context.User.Claims.ToList().Find(p => p.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
        return roleInContext;
    }
}