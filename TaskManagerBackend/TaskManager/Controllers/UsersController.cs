using Microsoft.AspNetCore.Authorization;
using TaskManager.Models.Project;
using TaskManager.Models.User;
using TaskManager.Service.Enums.Achievement;
using TaskManager.Service.Validation;

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
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        List<User> users;
        try
        {
            users = await _userService.GetAll();
        }
        catch (KeyNotFoundException exception)
        {
            return BadRequest(exception.Message);
        }

        var mappedUsers = users.Select(user => 
            _mapper.Map<UserDataModel>(user)
            ).ToList();

        return Ok(mappedUsers);
    }

    [HttpGet("{userId:int}/achievements")]
    [Produces("application/json")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetUserAchievements(
        int userId, 
        AchievementSortState sortState = AchievementSortState.IdDesc
        )
    {
        User user;
        try
        {
            user = await _userService.GetWithAchievementsById(userId, sortState);
        }
        catch (KeyNotFoundException exception)
        {
            return BadRequest(exception.Message);
        }

        var mappedData = _mapper.Map<UserAchievementsModel>(user);

        return Ok(mappedData);
    }

    [HttpGet("{userId:int}/projects")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetUserProjectsByUserId(int userId)
    {
        if (!await UserValidation.CheckUserIdentity(HttpContext, userId, _userService))
        {
            return Unauthorized();
        }

        User user;
        try
        {
            user = await _userService.GetWithProjectsById(userId);
        }
        catch (KeyNotFoundException exception)
        {
            return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
        }

        var mappedUser = _mapper.Map<UserProjectsModel>(user);

        return Ok(mappedUser);
    }
}