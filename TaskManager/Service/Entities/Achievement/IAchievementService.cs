using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Service.Entities.Achievement;

using TaskManager.Entities;

public interface IAchievementService
{
    Task<List<Achievement>> GetAll();
    Task<Achievement> GetById(int id);
    Task<Achievement> PostNew(Achievement achievement);
    Task<StatusCodeResult> UpdateAchievement(int id, Achievement achievement);
    Task<StatusCodeResult> DeleteAchievement(int id);
}