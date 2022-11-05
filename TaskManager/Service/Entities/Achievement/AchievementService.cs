using System.Data.Entity.Core;
using TaskManager.Service.Data.DbContext;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Achievement>> GetAll()
    {
        return await GetAchievements();
    }

    public async Task<Achievement> GetById(int id)
    {
        return await GetAchievement(id);
    }

    public async Task<Achievement> PostNew(Achievement achievement)
    {
        return await AddAchievement(achievement);
    }

    public async Task<StatusCodeResult> UpdateAchievement(int id, Achievement achievement)
    {
        return await UpdateAchievementById(id, achievement);
    }

    public async Task<StatusCodeResult> DeleteAchievement(int id)
    {
        return await DeleteAchievementById(id);
    }

    private async Task<List<Achievement>> GetAchievements()
    {
        var result = await _context.Achievements.ToListAsync().ConfigureAwait(false);

        if (!result.Any())
        {
            throw new ObjectNotFoundException("Achievements list is empty");
        }

        return result.ToList();
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

    private async Task<StatusCodeResult> UpdateAchievementById(int id, Achievement achievement)
    {
        _context.Entry(achievement).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            if (!AchievementExists(id))
            {
                return new NotFoundResult();
            }
            else
            {
                throw;
            }
        }

        return new NoContentResult();
    }

    private bool AchievementExists(int id)
    {
        return (_context.Achievements?.Any(p => p.AchievementId == id)).GetValueOrDefault();
    }

    private async Task<StatusCodeResult> DeleteAchievementById(int id)
    {
        var achievement = await _context.Achievements.FindAsync(id);
        if (achievement == null)
        {
            return new NotFoundResult();
        }

        _context.Achievements.Remove(achievement);
        await _context.SaveChangesAsync();

        return new NoContentResult();
    }
}