namespace TaskManager.Service.Entities.Achievement;

using TaskManager.Entities;

public interface IAchievementService
{
    IQueryable<Achievement> GetAll();
    Task<Achievement> GetById(int id);
    Task<Achievement> PostNew(Achievement achievement);
}