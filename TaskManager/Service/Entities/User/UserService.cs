using AutoMapper;
using TaskManager.Service.Data.DbContext;
using TaskManager.Service.User;

namespace TaskManager.Service.Entities.User;

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

    public IEnumerable<TaskManager.Entities.User> GetAll()
    {
        return _context.Users;
    }

    public TaskManager.Entities.User GetById(int id)
    {
        return GetUser(id);
    }

    public TaskManager.Entities.User GetByLoginData(UserLoginModel loginModel)
    {
        return GetUser(loginModel);
    }

    public IEnumerable<TaskManager.Entities.Achievement> GetUserAchievements(int userId)
    {
        return getUserAchievements(userId);
    }

    private IEnumerable<TaskManager.Entities.Achievement> getUserAchievements(int userId)
    {
        var query = _context
            .Users
            .Where(p => p.Id == userId)
            .SelectMany(p => p.Achievements);

        if (query == null) throw new KeyNotFoundException("User not found");

        return query;
    }

    private TaskManager.Entities.User GetUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    private TaskManager.Entities.User GetUser(UserLoginModel loginModel)
    {
        var user = _context.Users.FirstOrDefault(x =>
            x.UserEmail == loginModel.Email 
            && x.UserPassword == loginModel.Password
            );
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}