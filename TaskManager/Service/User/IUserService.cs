namespace TaskManager.Service.User;

using Entities;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User GetById(int id);
}