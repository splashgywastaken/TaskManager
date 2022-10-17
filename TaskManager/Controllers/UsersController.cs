namespace TaskManager.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Service.User;

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

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }
}