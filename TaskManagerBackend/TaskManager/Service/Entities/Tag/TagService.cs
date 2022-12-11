using System.Data.Entity.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using TaskManager.Service.Data.DbContext;

namespace TaskManager.Service.Entities.Tag;

using TaskManager.Entities;

public class TagService : ITagService
{
    private readonly TaskManagerDBContext _context;
    private readonly IMapper _mapper;

    public TagService(TaskManagerDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Tag>> GetAllTags()
    { 
        return await GetTags();
    }

    public async Task<Tag> GetById(int tagId)
    {
        return await GetTag(tagId);
    }

    public async Task<Tag> PostNew(Tag tag)
    {
        return await AddNew(tag);
    }

    public async Task<StatusCodeResult> UpdateTag(int id, Tag tag)
    {
        return await UpdateTagById(id, tag);
    }

    public Task<StatusCodeResult> DeleteTag(int id)
    {
        return DeleteById(id);
    }

    private async Task<IEnumerable<Tag>> GetTags()
    {
        if (!_context.Tags.Any())
        {
            throw new ObjectNotFoundException();
        }

        var result = await _context.Tags.ToListAsync();

        if (result == null) throw new ObjectNotFoundException("TasksTagsTags not found");

        return result;
    }

    private async Task<Tag> GetTag(int tagId)
    {
        var result = await _context.Tags.FirstOrDefaultAsync(p => p.TagId == tagId);

        if (result == null) throw new KeyNotFoundException("Tag not found");

        return result;
    }

    private async Task<Tag> AddNew(Tag tag)
    {
        var result = _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    private async Task<StatusCodeResult> UpdateTagById(int id, Tag tag)
    {
        _context.Entry(tag).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TagExists(id))
            {
                return new NotFoundResult();
            }
            throw;
        }

        return new NotFoundResult();
    }

    private bool TagExists(int id)
    {
        return (_context.Tags?.Any(p => p.TagId == id)).GetValueOrDefault();
    }

    private async Task<StatusCodeResult> DeleteById(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            return new NotFoundResult();
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return new NoContentResult();
    }
}