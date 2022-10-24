namespace TaskManager.Service.Entities.Achievement;

using TaskManager.Entities;

public interface IAchievementService
{
    IQueryable<Achievement> GetAll();
    Achievement GetById(int id);

}