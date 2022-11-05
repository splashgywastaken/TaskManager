using System.Data.Entity;
using TaskManager.Service.Data.DbContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;

namespace TaskManager.Service.Entities.Achievement;

using TaskManager.Entities;

public class AchievementService : IAchievementService
{
    private readonly TaskManagerDBContext _context;
    private readonly IMapper _mapper;

    public AchievementService(
        TaskManagerDBContext context,
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

    public async Task<Achievement> GetById(int id)
    {
        return await GetAchievement(id);
    }

    public async Task<Achievement> PostNew(Achievement achievement)
    {
        return await AddAchievement(achievement);
    }

    private IQueryable<Achievement> GetAchievements()
    {
        var result = _context.Achievements.AsQueryable();

        return result;
    }

    private async Task<Achievement> GetAchievement(int id)
    {
        var achievement = await _context.Achievements.FindAsync(id);
        if (achievement == null) throw new KeyNotFoundException("Achievement not found");

        return achievement;
    }

    private async Task<Achievement> AddAchievement(Achievement achievement)
    {
        _context.Achievements.Add(achievement);
        await _context.SaveChangesAsync();

        return achievement;
    } 
}