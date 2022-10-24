namespace TaskManager.Service.Entities.Project;

using TaskManager.Entities;

public interface IProjectService
{
    public IQueryable<Project> GetProjectsByUserId(int userId);
    public Project GetProjectById(int projectId);
}
