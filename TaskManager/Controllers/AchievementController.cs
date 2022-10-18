using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Entities;
using TaskManager.Service.Entities.Achievement;

namespace TaskManager.Controllers;

[Controller]
[Route("achievements")]
public class AchievementController : Controller
{
    private readonly IAchievementService _achievementService;
    private IMapper _mapper;

    public AchievementController(
        IAchievementService achievementService, 
        IMapper mapper
        )
    {
        _achievementService = achievementService;
        _mapper = mapper;
    }

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var achievements = _achievementService.GetAll();

        var achievementsList = achievements.ToList();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        
        var json = JsonSerializer.Serialize<List<Achievement>>(achievementsList, options);

        return Ok(json);
    }

    [HttpGet("")]
    public IActionResult GetById([FromQuery]int id)
    {
        var achievement = _achievementService.GetById(id);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize<Achievement>(achievement, options);

        return Ok(json);
    }
}