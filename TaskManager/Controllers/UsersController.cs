using TaskManager.Models.User;

namespace TaskManager.Controllers;

using System.Text.Json;
using Entities;
using TaskManager.Service.Entities.User;
using AutoMapper;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("user")]
[Produces("application/json")]
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

        return Ok(mappedUsers);
    }

    [HttpGet("{userId:int}/achievements")]
    [Produces("application/json")]
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

        return Ok(mappedData);
    }

    [HttpGet("{userId:int}")]
    [Produces("application/json")]
    public IActionResult GetById(int userId)
    {
        User user;
        try
        {
            user = _userService.GetById(userId);
        }
        catch (KeyNotFoundException exception)
        {
            return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
        }

        var mappedUser = _mapper.Map<UserDataModel>(user);

        return Ok(mappedUser);
    }

    [HttpGet("{userId:int}/projects")]
    public IActionResult GetUserProjectsByUserId(int userId)
    {
        User user;
        try
        {
            user = _userService.GetWithProjectsById(userId);
        }
        catch (KeyNotFoundException exception)
        {
            return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
        }

        var mappedUser = _mapper.Map<UserProjectsModel>(user);

        return Ok(mappedUser);
    }
}