using System.Data.Entity.Core;
using AutoMapper;
using TaskManager.Service.Data.DbContext;

namespace TaskManager.Service.Entities.Task;

using TaskManager.Entities;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public IQueryable<Task> GetAllProjectTasks(int projectId)
    {
        return GetProjectTasks(projectId);
    }

    public IQueryable<Tag> GetAllTaskTags(int taskId)
    {
        return GetTags(taskId);
    }

    public Task GetTaskById(int taskId)
    {
        throw new NotImplementedException();
    }

    private IQueryable<Task> GetProjectTasks(int projectId)
    {
        var result = _context.Tasks.Where(
                p => p.TaskGroup.ProjectId == projectId
            );

        return result;
    }

    private IQueryable<Tag> GetTags(int taskId)
    {
        var result = 
            from tag in _context.Tags
            join tasksTags in _context.TasksTags 
                on tag.TagId equals tasksTags.TagId
            where tasksTags.TaskId == taskId
                select tag;

        return result;
    }

    private Task GetTask(int taskId)
    {
        var result = _context.Tasks.FirstOrDefault(p => p.TaskId == taskId);

        if (result == null) throw new KeyNotFoundException("Task not found");

        return result;
    }
}