using System.Data.Entity.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Tag;
using TaskManager.Service.Data.DbContext;

namespace TaskManager.Service.Entities.Task;

using TaskManager.Entities;

public class TaskService : ITaskService
{
    private readonly TaskManagerDBContext _context;
    private readonly IMapper _mapper;

    public TaskService(TaskManagerDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<Tag>> GetAllTaskTags(int taskId)
    {
        return await GetTags(taskId);
    }

    public async Task<Task> GetTaskById(int taskId)
    {
        return await GetTask(taskId);
    }

    public async Task<StatusCodeResult> PutTask(int taskId, Task task)
    {
        return await UpdateTask(taskId, task);
    }

    private async Task<IEnumerable<Tag>> GetTags(int taskId)
    {
        return await GetTaskTagsById(taskId);
    }

    private async Task<Task> GetTask(int taskId)
    {
        var result = await _context.Tasks.FindAsync(taskId);

        if (result == null) throw new KeyNotFoundException("Task not found");

        return result;
    }
    
    private async Task<StatusCodeResult> UpdateTask(int taskId, Task task)
    {
        _context.Entry(task).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            if (!TaskExists(taskId))
            {
                return new NotFoundResult();
            }

            throw;
        }

        return new NoContentResult();
    }

    private bool TaskExists(int taskId)
    {
        return (_context.Tasks?.Any(p => p.TaskId == taskId)).GetValueOrDefault();
    }

    private async Task<IEnumerable<Tag>> GetTaskTagsById(int id)
    {
        var result = await (
                from task in _context.Tasks
                join tasksTags in _context.TasksTags
                    on task.TaskId equals tasksTags.TasksTagsTaskId
                join tag in _context.Tags
                    on tasksTags.TasksTagsTagId equals tag.TagId
                where task.TaskId == id
                    select tag
                ).ToListAsync();

        return result;
    }
}