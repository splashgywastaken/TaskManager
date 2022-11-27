using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Service.Entities.Task;

using TaskManager.Entities;

public interface ITaskService
{
    Task<IEnumerable<Tag>> GetAllTaskTags(int taskId);
    Task<Task> GetTaskById(int taskId);
    Task<StatusCodeResult> PutTask(int taskId, Task task);
}