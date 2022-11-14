using TaskManager.Models.Project;
using TaskManager.Models.User;
using TaskManager.Service.Enums.Achievement;

namespace TaskManager.Service.Entities.User;

using TaskManager.Entities;
public interface IUserService
{
    Task<List<User>> GetAll();
    Task<User> GetById(int id);
    Task<User> GetWithAchievementsById(int id, AchievementSortState sortState);
    Task<User> GetWithProjectsById(int userId);
    Task<User> GetByLoginData(UserLoginModel loginModel);
    Task<List<Achievement>> GetUserAchievements(int userId);
}