namespace TaskManager.Service.Entities.Achievement;

using TaskManager.Entities;

public interface IAchievementService
{
    IEnumerable<Achievement> GetAll();
    Achievement GetById(int id);
}