using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Migrations;
using TaskManager.Service.Data.DbContext;

namespace TaskManager.Service.Entities.TaskGroup;
using TaskManager.Entities;

public class TaskGroupService : ITaskGroupService
{
    private readonly TaskManagerDBContext _context;
    private readonly IMapper _mapper;

    public TaskGroupService(TaskManagerDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskGroup> GetById(int id)
    {
        return await GetTaskGroupById(id);
    }

    public async Task<TaskGroup> PostTaskGroup(TaskGroup taskGroup)
    {
        return await AddNewTaskGroup(taskGroup);
    }

    private async Task<TaskGroup> AddNewTaskGroup(TaskGroup taskGroup)
    {
        await _context.Projects.FindAsync(taskGroup.TaskGroupProjectId);

        _context.TaskGroups.Add(taskGroup);
        await _context.SaveChangesAsync();

        return taskGroup;
    }

    public async Task<StatusCodeResult> UpdateTaskGroup(int taskGroupId, TaskGroup taskGroup)
    {
        return await UpdateTaskGroupById(taskGroupId, taskGroup);
    }

    public async Task<StatusCodeResult> DeleteTaskGroup(int taskGroupId)
    {
        return await DeleteTaskGroupById(taskGroupId);
    }

    private async Task<StatusCodeResult> DeleteTaskGroupById(int taskGroupId)
    {
        var taskGroup = await _context.TaskGroups
            .Include(tg => tg.Tasks)
            .FirstOrDefaultAsync(tg => tg.TaskGroupId == taskGroupId);

        if (taskGroup == null) return new NotFoundResult();

        foreach (var task in taskGroup.Tasks)
        {
            _context.Tasks.Remove(task);
        }
        _context.TaskGroups.Remove(taskGroup);

        await _context.SaveChangesAsync();

        return new NoContentResult();
    }

    private async Task<StatusCodeResult> UpdateTaskGroupById(int taskGroupId, TaskGroup taskGroup)
    {
        _context.Entry(taskGroup).State = EntityState.Modified;
        _context.TaskGroups.Update(taskGroup);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            if (!TaskGroupExists(taskGroupId))
            {
                return new NotFoundResult();
            }

            throw;
        }

        return new NoContentResult();
    }

    private bool TaskGroupExists(int taskGroupId)
    {
        return (_context.TaskGroups?.Any(p => p.TaskGroupId == taskGroupId)).GetValueOrDefault();
    }

    private async Task<TaskGroup> GetTaskGroupById(int id)
    {
        var taskGroup = await _context.TaskGroups.FindAsync(id);

        if (taskGroup == null) throw new KeyNotFoundException("Task group not found");

        return taskGroup;
    }

}