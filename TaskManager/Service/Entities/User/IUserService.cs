using TaskManager.Service.User;

namespace TaskManager.Service.Entities.User;

using TaskManager.Entities;
public interface IUserService
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    User GetByLoginData(UserLoginModel loginModel);
    IEnumerable<Achievement> GetUserAchievements(int userId);
}