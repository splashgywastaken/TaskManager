using TaskManager.Service.User;

namespace TaskManager.Service.Entities.User;

public interface IUserService
{
    IEnumerable<TaskManager.Entities.User> GetAll();
    TaskManager.Entities.User GetById(int id);
    TaskManager.Entities.User GetByLoginData(UserLoginModel loginModel);
    IEnumerable<TaskManager.Entities.Achievement> GetUserAchievements(int userId);
}