using System.Data.Entity.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

    // auth
    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        // Получаем данные из формы запроса для авторизации/аутентификации
        var loginModel = new UserLoginModel(email,password);

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
            new (ClaimsIdentity.DefaultNameClaimType, user.UserEmail),
            new (ClaimsIdentity.DefaultRoleClaimType, user.UserRole)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync(claimsPrincipal);

        return Ok(loginModel);
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

    [HttpDelete("delete")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> Delete(int userId)
    {
        User user;
        try
        {
            user = await _userService.GetById(userId);
        }
        catch (KeyNotFoundException)
        {
            return BadRequest();
        }

        var userIdCorrespondsUserIdInContext = false;
        try
        {
            userIdCorrespondsUserIdInContext =
                await UserValidation.CheckUserIdentity(HttpContext, userId, _userService);
        }
        catch (ObjectNotFoundException) { }

        var userRole = UserValidation.GetUserRole(HttpContext);

        if (!(userRole == "admin" || userIdCorrespondsUserIdInContext))
        {
            return new UnauthorizedResult();
        }

        return await _userService.DeleteUser(userId);
    }
}