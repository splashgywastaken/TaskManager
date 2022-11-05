using System.Data.Entity.Core;
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
    public async Task<IActionResult> GetAll()
    {
        List<Achievement> achievements;
        try
        {
            achievements = await _achievementService.GetAll();
        }
        catch (ObjectNotFoundException exception)
        {
            return NotFound(exception.Message);
        }

        var mappedAchievementsList = achievements.Select(achievement =>
            _mapper.Map<AchievementModel>(achievement)
            ).ToList();

        return Ok(mappedAchievementsList);
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

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> PutAchievement(int id, [FromBody] AchievementModel achievement)
    {
        if (id != achievement.AchievementId)
        {
            return BadRequest();
        }

        var mappedAchievement = _mapper.Map<Achievement>(achievement);

        var status = await _achievementService.UpdateAchievement(id, mappedAchievement);

        if (status.StatusCode == StatusCodes.Status204NoContent)
        {
            return NoContent();
        } 

        return NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAchievement(int id)
    {
        var result = await _achievementService.DeleteAchievement(id);

        if (result.StatusCode == StatusCodes.Status404NotFound)
        {
            return NotFound();
        }

        return NoContent();
    }
}