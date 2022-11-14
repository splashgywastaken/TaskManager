using System.Data.Entity.Core;
using TaskManager.Service.Data.DbContext;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace TaskManager.Service.Entities.Project;

using TaskManager.Entities;

public class ProjectService : IProjectService
{
    private readonly TaskManagerDBContext _context;
    private readonly IMapper _mapper;

    public ProjectService(TaskManagerDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await GetAll();
    }

    public async Task<IEnumerable<Project>> GetProjectsByUserId(int userId)
    {
        return await GetProjects(userId);
    }

    public async Task<IEnumerable<Task>> GetAllProjectTasks(int projectId)
    {
        return await GetAllProjectTasksById(projectId);
    }

    public async Task<Project> GetProjectById(int projectId)
    {
        return await GetProject(projectId);
    }

    public async Task<Project> PostNewUserProject(Project project)
    {
        return await AddNewUserProject(project);
    }

    public async Task<StatusCodeResult> UpdateProject(int projectId, Project project)
    {
        return await UpdateProjectById(projectId, project);
    }
    
    public async Task<StatusCodeResult> DeleteProject(int projectId)
    {
        return await DeleteProjectById(projectId);
    }

    private async Task<IEnumerable<Project>> GetAll()
    {
        if (!_context.Projects.Any())
        {
            throw new ObjectNotFoundException();
        }

        var projects = await _context.Projects.ToListAsync();

        return projects;
    }

    private async Task<IEnumerable<Project>> GetProjects(int userId)
    {
        var result = await _context.Projects.Where(
                p => p.ProjectUserId == userId
            ).ToListAsync();

        return result;
    }
    
    private async Task<IEnumerable<Task>> GetAllProjectTasksById(int projectId)
    {
        var result = await _context.Tasks.Where(p => 
                    p.TaskTaskGroup!.TaskGroupProjectId == projectId
            ).ToListAsync();
        
        return result;
    }

    private async Task<Project> GetProject(int projectId)
    {
        var result = await _context.Projects.FirstOrDefaultAsync(
            p => p.ProjectId == projectId
        );

        if (result == null) throw new KeyNotFoundException("TaskGroupProject not found");

        return result;
    }

    private async Task<Project> AddNewUserProject(Project project)
    {
        await _context.Users.FindAsync(project.ProjectUserId);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return project;
    }

    private async Task<StatusCodeResult> UpdateProjectById(int projectId, Project project)
    {
        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            if (!ProjectExists(projectId))
            {
                return new NotFoundResult();
            }

            throw;
        }

        return new NoContentResult();
    }
    
    private bool ProjectExists(int projectId)
    {
        return (_context.Projects?.Any(p => p.ProjectId == projectId)).GetValueOrDefault();
    }

    private async Task<StatusCodeResult> DeleteProjectById(int projectId)
    {
        var project = await _context.Projects.FindAsync(projectId);
        if (project == null)
        {
            return new NotFoundResult();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return new NoContentResult();
    }
}