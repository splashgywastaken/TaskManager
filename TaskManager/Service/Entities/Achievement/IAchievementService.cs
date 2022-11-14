using Microsoft.AspNetCore.Mvc;
using TaskManager.Service.Enums.Achievement;

namespace TaskManager.Service.Entities.Achievement;

using TaskManager.Entities;

public interface IAchievementService
{
    Task<List<Achievement>> GetAll(AchievementSortState sortState);
    Task<Achievement> GetById(int id);
    Task<Achievement> PostNew(Achievement achievement);
    Task<StatusCodeResult> UpdateAchievement(int id, Achievement achievement);
    Task<StatusCodeResult> DeleteAchievement(int id);
}