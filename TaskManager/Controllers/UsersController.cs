using TaskManager.Models.User;

namespace TaskManager.Controllers;

using System.Text.Json;
using Entities;
using TaskManager.Service.Entities.User;
using AutoMapper;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("user")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(
        IUserService userService,
        IMapper mapper
    )
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("all")]
    //[Produces("application/json")]
    public IActionResult GetAll()
    {
        IQueryable<User> users;
        try
        {
            users = _userService.GetAll();
        }
        catch (KeyNotFoundException exception)
        {
            return BadRequest(exception.Message);
        }

        var mappedUsers = new List<UserDataModel>();

        foreach (var user in users)
        {
            mappedUsers.Add(
                _mapper.Map<UserDataModel>(user)
                );
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(mappedUsers, options);

        return Ok(json);
    }

    [HttpGet("{userId:int}/achievements")]
    //[Produces("application/json")]
    public IActionResult GetUserAchievements(int userId)
    {
        User user;
        try
        {
            user = _userService.GetWithAchievementsById(userId);
        }
        catch (KeyNotFoundException exception)
        {
            return BadRequest(exception.Message);
        }

        var mappedData = _mapper.Map<UserAchievementsModel>(user);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(mappedData, options);

        return Ok(json);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        User user;
        try
        {
            user = _userService.GetById(id);
        }
        catch (KeyNotFoundException exception)
        {
            return BadRequest(exception.Message);
        }

        var mappedUser = _mapper.Map<UserDataModel>(user);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(mappedUser, options);

        return Ok(json);
    }
}