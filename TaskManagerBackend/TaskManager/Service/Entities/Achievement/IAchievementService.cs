using Microsoft.AspNetCore.Mvc;
using TaskManager.Service.Enums.Achievement;
using TaskManager.Service.Enums.Search;

namespace TaskManager.Service.Entities.Achievement;

using TaskManager.Entities;

public interface IAchievementService
{
    Task<List<Achievement>> GetAll(AchievementSortState sortState);
    Task<Achievement> GetById(int id);
    Task<List<Achievement>> FindByName(string name, SearchType searchType);
    Task<Achievement> PostNew(Achievement achievement);
    Task<StatusCodeResult> UpdateAchievement(int id, Achievement achievement);
    Task<StatusCodeResult> DeleteAchievement(int id);
}