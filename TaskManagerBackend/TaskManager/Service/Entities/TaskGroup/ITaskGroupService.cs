using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Service.Entities.TaskGroup;
using TaskManager.Entities;

public interface ITaskGroupService
{
    Task<TaskGroup> GetById(int id);
    Task<TaskGroup> PostTaskGroup(TaskGroup taskGroup);
    Task<StatusCodeResult> UpdateTaskGroup(int taskGroupId, TaskGroup taskGroup);
    Task<StatusCodeResult> DeleteTaskGroup(int taskGroupId);
}