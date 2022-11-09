using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Service.Entities.Project;

using TaskManager.Entities;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjects();
    Task<IEnumerable<Project>> GetProjectsByUserId(int userId);
    Task<Project> GetProjectById(int projectId);
    Task<StatusCodeResult> UpdateProject(int projectId, Project project);
    Task<StatusCodeResult> DeleteProject(int projectId);
}
