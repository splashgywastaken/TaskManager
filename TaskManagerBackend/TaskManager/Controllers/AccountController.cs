using System.Data.Entity.Core;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using TaskManager.Models.User;
using TaskManager.Service.Exception.CRUD;
using TaskManager.Service.Validation;

namespace TaskManager.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Service.Entities.User;

[ApiController]
[AllowAnonymous]
[Route("user")]
public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AccountController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    /// Main auth method
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns>
    /// If login is successfull then returns current user data, else return Unauthorized code status
    /// </returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginModel loginModel)
    {
        User user;
        try
        {
            user = await _userService.GetByLoginData(loginModel);
        }
        catch (KeyNotFoundException)
        {
            return Unauthorized(loginModel);
        }

        // Создаём клеймы
        var claims = new List<Claim>
        {
            new ("UserId", user.UserId.ToString(), ClaimValueTypes.String),
            new (ClaimTypes.NameIdentifier, user.UserEmail, ClaimValueTypes.String),
            new (ClaimTypes.Name, user.UserName, ClaimValueTypes.String),
            new (ClaimTypes.Role, user.UserRole, ClaimValueTypes.String)
        };

        var userIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        userIdentity.AddClaims(claims);

        var principal = new ClaimsPrincipal(userIdentity);
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.Now.AddHours(12)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            principal,
            authProperties
            );

        SetPrincipalAndIdentity(principal);

        //var principal = new ClaimsPrincipal(userIdentity);
        //var authProperties = new AuthenticationProperties
        //{
        //    ExpiresUtc = DateTimeOffset.Now.AddDays(1),
        //    IsPersistent = true,
        //    AllowRefresh = true
        //};

        //try
        //{
        //    await HttpContext.SignInAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        new ClaimsPrincipal(principal),
        //        authProperties
        //    );
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //    throw;
        //}

        if (!HttpContext.User.Identity!.IsAuthenticated)
        {
            var message = new
            {
                message = "unauthorized",
                login_model = loginModel
            };
            return Unauthorized(message);
        }

        // Возвращаем данные о пользователе
        var userData = _mapper.Map<UserDataModel>(user);
        return Ok(userData);
    }

    private void SetPrincipalAndIdentity(IPrincipal principal)
    {
        Thread.CurrentPrincipal = principal;
        HttpContext.User = (ClaimsPrincipal) principal;
    }

    [HttpGet("{userId:int}")]
    [Produces("application/json")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetById(int userId)
    {
        User user;
        try
        {
            user = await _userService.GetById(userId);
        }
        catch (KeyNotFoundException exception)
        {
            return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
        }

        var mappedUser = _mapper.Map<UserDataModel>(user);

        return Ok(mappedUser);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]UserRegistrationModel registrationModel)
    {
        User user;
        try
        {
            user = await _userService.PostUser(registrationModel);
        }
        catch (EmailIsNotUniqueException exception)
        {
            var message = new
            {
                message = "Email is not unique",
                exception_message = exception.Message
            };

            return BadRequest(message);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException exception)
        {
            var message = new
            {
                message = "Exception triggered on Db update",
                exception_message = exception.Message
            };

            return BadRequest(message);
        }

        return CreatedAtAction(
            nameof(GetById),
            new { userId = user.UserId},
            user
        );
    }

    [HttpPost("logout")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    //[Authorize(Roles = "admin, user")]
    public async Task<IActionResult> Delete(int id)
    {
        User user;
        try
        {
            user = await _userService.GetById(id);
        }
        catch (KeyNotFoundException)
        {
            return BadRequest();
        }

        // Bring that back when you will fix auth
        //var userIdCorrespondsUserIdInContext = false;
        //try
        //{
        //    userIdCorrespondsUserIdInContext =
        //        await UserValidation.CheckUserIdentity(HttpContext, id, _userService);
        //}
        //catch (ObjectNotFoundException) { }

        //var userRole = UserValidation.GetUserRole(HttpContext);

        //if (!(userRole == "admin" || userIdCorrespondsUserIdInContext))
        //{
        //    return new UnauthorizedResult();
        //}

        return await _userService.DeleteUser(id);
    }
}