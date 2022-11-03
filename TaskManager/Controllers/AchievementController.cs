using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Entities;
using TaskManager.Models.Achievement;
using TaskManager.Service.Entities.Achievement;

namespace TaskManager.Controllers;

[Controller]
[Route("achievements")]
[Produces("application/json")]
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

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var achievements = _achievementService.GetAll();
        
        var mappedAchievementList = achievements.ToList().Select(achievement => 
            _mapper.Map<AchievementModel>(achievement)
            );

        return Ok(mappedAchievementList);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var achievement = _achievementService.GetById(id);

        var mappedAchievement = _mapper.Map<AchievementModel>(achievement);

        return Ok(mappedAchievement);
    }
}