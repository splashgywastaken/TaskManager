using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using TaskManager.Service.Data.DbContext;

namespace TaskManager.Service.Entities.Project;

using TaskManager.Entities;

public class ProjectService : IProjectService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public IQueryable<Project> GetProjectsByUserId(int userId)
    {
        return GetProjects(userId);
    }

    public Project GetProjectById(int projectId)
    {
        return GetProject(projectId);
    }

    private IQueryable<Project> GetProjects(int userId)
    {
        var result = _context.Projects.Where(
                p => p.UserId == userId
            );

        return result;
    }

    private Project GetProject(int projectId)
    {
        var result = _context.Projects.FirstOrDefault(
            p => p.ProjectId == projectId
        );

        if (result == null) throw new KeyNotFoundException("Project not found");

        return result;
    }
}