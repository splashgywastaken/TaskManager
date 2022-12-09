using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Models.User;

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

    [HttpPost("/logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
}