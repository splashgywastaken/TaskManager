using System.Data.Entity.Core;
using AutoMapper;
using TaskManager.Service.Data.DbContext;

namespace TaskManager.Service.Entities.Tag;

using TaskManager.Entities;

public class TagService : ITagService
{
    private readonly TaskManagerDBContext _context;
    private readonly IMapper _mapper;

    public IQueryable<Tag> GetAllTags()
    { 
        return GetTags();
    }

    public Tag GetById(int tagId)
    {
        return GetTag(tagId);
    }

    private IQueryable<Tag> GetTags()
    {
        var result = _context.Tags.AsQueryable();

        if (result == null) throw new ObjectNotFoundException("TasksTagsTags not found");

        return result;
    }

    private Tag GetTag(int tagId)
    {
        var result = _context.Tags.FirstOrDefault(p => p.TagId == tagId);

        if (result == null) throw new KeyNotFoundException("Tag not found");

        return result;
    }
}