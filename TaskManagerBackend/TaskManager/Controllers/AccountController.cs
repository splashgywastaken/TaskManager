using TaskManager.Models.User;

namespace TaskManager.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Service.Entities.User;

[ApiController]
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
    public async Task<IActionResult> Login([FromBody] UserLoginModel loginModel)
    {
        var user = await _userService.GetByLoginData(loginModel);

        if (user is null) return Unauthorized(loginModel);

        var claims = new List<Claim> { new(ClaimTypes.Name, user.UserEmail) };
        // Создаём JWT - токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISUSER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(
                AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256
            )
        );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            username = user.UserEmail
        };

        return Ok(response);
    }
}