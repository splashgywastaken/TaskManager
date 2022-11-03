using TaskManager.Models.User;

namespace TaskManager.Service.Entities.User;

using TaskManager.Entities;
public interface IUserService
{
    IQueryable<User> GetAll();
    User GetById(int id);
    User GetWithAchievementsById(int id);
    User GetWithProjectsById(int userId);
    User GetByLoginData(UserLoginModel loginModel);
    IQueryable<Achievement> GetUserAchievements(int userId);
}