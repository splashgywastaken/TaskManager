namespace TaskManager.Service.Entities.Task;

using TaskManager.Entities;

public interface ITaskService
{
    IQueryable<Task> GetAllProjectTasks(int projectId);
    IQueryable<Tag> GetAllTaskTags(int taskId);
    Task GetTaskById(int taskId);
}