using System.Data.Entity;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using TaskManager.Models.User;
using TaskManager.Service.Data.DbContext;

namespace TaskManager.Service.Entities.User;

using TaskManager.Entities;

public class UserService : IUserService
{
    private readonly TaskManagerDBContext _context;
    private readonly IMapper _mapper;

    public UserService(
        TaskManagerDBContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public IQueryable<User> GetAll()
    {
        return _context.Users;
    }

    public User GetById(int id)
    {
        return GetUser(id);
    }

    public User GetWithAchievementsById(int id)
    {
        return GetUserWithAchievements(id);
    }

    public User GetWithProjectsById(int userId)
    {
        return GetUserWithProjects(userId);
    }

    public User GetByLoginData(UserLoginModel loginModel)
    {
        return GetUser(loginModel);
    }

    public IQueryable<Achievement> GetUserAchievements(int userId)
    {
        return GetAchievementsByUserId(userId);
    }

    private IQueryable<Achievement> GetAchievementsByUserId(int userId)
    {
        return null;
    }

    private User GetUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
    
    private User GetUserWithAchievements(int id)
    {
        var query =
            from user in _context.Users
            join ua in _context.UsersAchievement
                on user.UserId equals ua.UserId
            join achievement in _context.Achievements
                on ua.AchievementId equals achievement.AchievementId
            where user.UserId == id
                select new { user, achievement};

        if (query == null) throw new KeyNotFoundException("User not found");

        var resultUser = query.First().user;
        foreach (var dataRow in query)
        {
            resultUser.Achievements.Add(dataRow.achievement);
        }

        return resultUser;
    }

    private User GetUserWithProjects(int userId)
    {
        var query =
            from user in _context.Users
            where user.UserId == userId
            join projects in _context.Projects
                on user.UserId equals projects.UserId
            select new { user, projects };

        if (query == null) throw new KeyNotFoundException("User not founds");

        var resultUser = query.First().user;
        foreach (var dataRow in query)
        {
            resultUser.Projects.Add(dataRow.projects);
        }

        return resultUser;
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