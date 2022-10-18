using TaskManager.Service.Data.DbContext;
using AutoMapper;

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

    public IEnumerable<Achievement> GetAll()
    {
        return _context.Achievements;
    }

    public TaskManager.Entities.Achievement GetById(int id)
    {
        return GetAchievement(id);
    }

    private TaskManager.Entities.Achievement GetAchievement(int id)
    {
        var achievement = _context.Achievements.FirstOrDefault(a => a.AchievementId == id);
        if (achievement == null) throw new KeyNotFoundException("Achievement not found");

        return achievement;
    }
}