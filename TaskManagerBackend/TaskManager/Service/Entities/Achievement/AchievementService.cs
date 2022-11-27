using System.Data.Entity.Core;
using System.Runtime.InteropServices;
using TaskManager.Service.Data.DbContext;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Service.Enums.Achievement;
using TaskManager.Service.Enums.Search;

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

    public async Task<List<Achievement>> GetAll(AchievementSortState sortState)
    {
        return await GetAchievements(sortState);
    }

    public async Task<Achievement> GetById(int id)
    {
        return await GetAchievement(id);
    }

    public async Task<List<Achievement>> FindByName(string name, SearchType searchType)
    {
        return await GetAchievement(name, searchType);
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

    private async Task<List<Achievement>> GetAchievements(AchievementSortState sortState)
    {
        var achievements = sortState switch
        {
            AchievementSortState.NameAsc =>
                _context.Achievements.OrderBy(p => p.AchievementName),
            AchievementSortState.NameDesc =>
                _context.Achievements.OrderByDescending(p => p.AchievementName),
            AchievementSortState.IdAsc => 
                _context.Achievements.OrderBy(p => p.AchievementId),
            AchievementSortState.IdDesc =>
                _context.Achievements.OrderByDescending(p => p.AchievementId),
            AchievementSortState.ScoreAsc =>
                _context.Achievements.OrderBy(p => p.AchievementPoints),
            AchievementSortState.ScoreDesc =>
                _context.Achievements.OrderByDescending(p => p.AchievementPoints),
            _ => throw new ArgumentOutOfRangeException(nameof(sortState), sortState, null)
        };

        var result = await achievements.ToListAsync();

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

    /// <summary>
    /// Returns first found achievement that corresponds to arguments
    /// </summary>
    /// <param name="name"></param>
    /// <param name="searchType"></param>
    /// <returns>Achievement</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ObjectNotFoundException"></exception>
    private async Task<List<Achievement>> GetAchievement(string name, SearchType searchType)
    {
        List<Achievement> achievements;
        var contextAchievements = _context.Achievements;
        {
            achievements = await (searchType switch
            {
                SearchType.FullMatch => contextAchievements.Where(p =>
                    string.Equals(p.AchievementName.ToLower(), name.ToLower())
                ),
                SearchType.PartialMatch => contextAchievements.Where(p =>
                    p.AchievementName.ToLower().Contains(name.ToLower())
                ),
                SearchType.LetterCaseFullMatch => contextAchievements.Where(p =>
                    string.Equals(p.AchievementName, name)
                ),
                SearchType.LetterCasePartialMatch => contextAchievements.Where(p =>
                    p.AchievementName.Contains(name)
                ),
                _ => throw new ArgumentOutOfRangeException(nameof(searchType), searchType, null)
            }).ToListAsync();
        }

        if (!achievements.Any()) throw new ObjectNotFoundException();

        return achievements;
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
            throw;
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