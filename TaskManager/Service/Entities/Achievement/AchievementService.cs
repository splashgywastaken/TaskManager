using TaskManager.Service.Data.DbContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;

namespace TaskManager.Service.Entities.Achievement;

using TaskManager.Entities;

public class AchievementService : IAchievementService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AchievementService(
        ApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public IQueryable<Achievement> GetAll()
    {
        return GetAchievements();
    }

    public Achievement GetById(int id)
    {
        return GetAchievement(id);
    }

    private IQueryable<Achievement> GetAchievements()
    {
        var result = _context.Achievements.AsQueryable();

        return result;
    }

    private Achievement GetAchievement(int id)
    {
        var achievement = _context.Achievements.FirstOrDefault(a => a.AchievementId == id);
        if (achievement == null) throw new KeyNotFoundException("Achievement not found");

        return achievement;
    }
}