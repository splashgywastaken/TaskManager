namespace TaskManager.Controllers;

using System.Text.Json;
using Entities;
using TaskManager.Service.Entities.User;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("user")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private IMapper _mapper;

    public UsersController(
        IUserService userService,
        IMapper mapper
    )
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("all")]
    [Authorize]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();

        var response = new
        {
            message = users.ToString()
        };

        return Ok(response);
    }

    [HttpGet("{userId:int}/achievements")]
    public IActionResult GetUserAchievements(int userId)
    {
        var data = _userService.GetUserAchievements(userId).ToList();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize<List<Achievement>>(data, options);

        return Ok(json);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }
}