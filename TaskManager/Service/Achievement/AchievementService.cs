namespace TaskManager.Service.Achievement;

using AutoMapper;
using DbContext;
using Entities;

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

    public Achievement GetById(int id)
    {
        return GetAchievement(id);
    }

    private Achievement GetAchievement(int id)
    {
        var achievement = _context.Achievements.FirstOrDefault(a => a.AchievementId == id);
        if (achievement == null) throw new KeyNotFoundException("Achievement not found");

        return achievement;
    }
}