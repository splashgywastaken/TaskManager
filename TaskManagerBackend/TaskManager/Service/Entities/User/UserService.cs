using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Project;
using TaskManager.Models.User;
using TaskManager.Service.Data.DbContext;
using TaskManager.Service.Enums.Achievement;

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

    public async Task<List<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetById(int id)
    {
        return await GetUser(id);
    }

    public async Task<User> GetWithAchievementsById(int id, AchievementSortState sortState)
    {
        return await GetUserWithAchievements(id, sortState);
    }

    public async Task<User> GetWithProjectsById(int userId)
    {
        return await GetUserWithProjects(userId);
    }

    public async Task<User> GetByLoginData(UserLoginModel loginModel)
    {
        return await GetUser(loginModel);
    }

    public async Task<List<Achievement>> GetUserAchievements(int userId)
    {
        return await GetAchievementsByUserId(userId);
    }

    public async Task<Project> PostNewUserProject(Project project)
    {
        return await AddNewUserProject(project);
    }

    private async Task<Project> AddNewUserProject(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return project;
    }

    private async Task<List<Achievement>> GetAchievementsByUserId(int userId)
    {
        var queryResult =
            await (
                from achievement in _context.Achievements
                join ua in _context.UsersAchievement
                    on achievement.AchievementId equals ua.UsersAchievementsAchievementId
                where ua.UsersAchievementsUserId == userId
                select achievement
                ).ToListAsync();

        if (queryResult == null) throw new KeyNotFoundException("User not found");

        return queryResult;
    }

    private async Task<User> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new KeyNotFoundException("ProjectUser not found");
        return user;
    }
    
    private async Task<User> GetUserWithAchievements(int id, AchievementSortState sortState)
    {
        var queryResult =
            await (
                from user in _context.Users
                join ua in _context.UsersAchievement
                    on user.UserId equals ua.UsersAchievementsUserId
                join achievement in _context.Achievements
                    on ua.UsersAchievementsAchievementId equals achievement.AchievementId
                where user.UserId == id
                select new { user, achievement }
                ).ToListAsync();

        if (!queryResult.Any()) throw new KeyNotFoundException("ProjectUser not found");

        var resultUser = queryResult.First().user;
        foreach (var dataRow in queryResult)
        {
            resultUser.UsersAchievementsAchievements.Add(dataRow.achievement);
        }

        resultUser.UsersAchievementsAchievements = sortState switch
        {
            AchievementSortState.NameAsc =>
                resultUser.UsersAchievementsAchievements.OrderBy(p => p.AchievementName).ToList(),
            AchievementSortState.NameDesc =>
                resultUser.UsersAchievementsAchievements.OrderByDescending(p => p.AchievementName).ToList(),
            AchievementSortState.IdAsc =>
                resultUser.UsersAchievementsAchievements.OrderBy(p => p.AchievementId).ToList(),
            AchievementSortState.IdDesc =>
                resultUser.UsersAchievementsAchievements.OrderByDescending(p => p.AchievementId).ToList(),
            AchievementSortState.ScoreAsc =>
                resultUser.UsersAchievementsAchievements.OrderBy(p => p.AchievementPoints).ToList(),
            AchievementSortState.ScoreDesc =>
                resultUser.UsersAchievementsAchievements.OrderByDescending(p => p.AchievementPoints).ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(sortState), sortState, null)
        };

        return resultUser;
    }

    private async Task<User> GetUserWithProjects(int userId)
    {
        var queryResult = await _context.Users.Include(p =>
            p.Projects).Where(p => 
            p.UserId == userId).ToListAsync();

        if (!queryResult.Any()) throw new KeyNotFoundException();

        return queryResult.First();
    }
    private async Task<User> GetUser(UserLoginModel loginModel)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x =>
            x.UserEmail == loginModel.Email 
            && x.UserPassword == loginModel.Password
            );
        if (user == null) throw new KeyNotFoundException("ProjectUser not found");
        return user;
    }
}