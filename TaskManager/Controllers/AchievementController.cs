using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.Achievement;
using TaskManager.Service.Entities.Achievement;

namespace TaskManager.Controllers;

[Controller]
[Route("achievements")]
[Produces("application/json")]
public class AchievementController : Controller
{
    private readonly IAchievementService _achievementService;
    private readonly IMapper _mapper;

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
    public async Task<IActionResult> GetById(int id)
    {
        Achievement achievement;
        try
        {
            achievement = await _achievementService.GetById(id);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }

        var mappedAchievement = _mapper.Map<AchievementModel>(achievement);

        return Ok(mappedAchievement);
    }

    [HttpPost]
    public async Task<IActionResult> PostNewAchievement([FromBody] AchievementModel achievementModel)
    {
        var mappedAchievement = _mapper.Map<Achievement>(achievementModel);

        var resultAchievement = await _achievementService.PostNew(mappedAchievement);
        var mappedResultAchievement = _mapper.Map<AchievementModel>(resultAchievement);

        return CreatedAtAction(
            nameof(GetById), 
            new { id = mappedResultAchievement.AchievementId },
            mappedResultAchievement
            );
    }
}