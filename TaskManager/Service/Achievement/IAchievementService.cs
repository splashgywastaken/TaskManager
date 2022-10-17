namespace TaskManager.Service.Achievement;

using Entities;

public interface IAchievementService
{
    IEnumerable<Achievement> GetAll();
    Achievement GetById(int id);
}