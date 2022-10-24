using AutoMapper;
using TaskManager.Service.Data.DbContext;
using TaskManager.Service.User;

namespace TaskManager.Service.Entities.User;

using TaskManager.Entities;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(
        ApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User GetById(int id)
    {
        return GetUser(id);
    }

    public User GetByLoginData(UserLoginModel loginModel)
    {
        return GetUser(loginModel);
    }

    public IEnumerable<Achievement> GetUserAchievements(int userId)
    {
        return GetAchievementsByUserId(userId);
    }

    private IEnumerable<Achievement> GetAchievementsByUserId(int userId)
    {
        var query = _context
            .Users
            .Where(p => p.Id == userId)
            .SelectMany(p => p.Achievements);

        if (query == null) throw new KeyNotFoundException("User not found");

        return query;
    }

    private User GetUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    private User GetUser(UserLoginModel loginModel)
    {
        var user = _context.Users.FirstOrDefault(x =>
            x.UserEmail == loginModel.Email 
            && x.UserPassword == loginModel.Password
            );
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}