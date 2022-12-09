using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using TaskManager.Models.CompositeModels;
using TaskManager.Models.Project;
using TaskManager.Models.Task;
using TaskManager.Models.TaskGroup;
using TaskManager.Service.Entities.Project;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Controllers;

[ApiController]
[Produces("application/json")]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;

    public ProjectController(IProjectService projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    [Route("project/all")]
    public async Task<IActionResult> GetAll()
    {
        List<Project> projects;
        try
        {
            projects = (List<Project>) await _projectService.GetAllProjects();
        }
        catch (ObjectNotFoundException exception)
        {
            return NotFound(exception.Data);
        }

        var mappedProjects = projects.Select(p =>
            _mapper.Map<ProjectDataModel>(p)
        );

        return Ok(mappedProjects);
    }

    [HttpGet]
    [Authorize(Roles = "admin, user")]
    [Route("project/{projectId:int}/tasks")]
    public async Task<IActionResult> GetAllProjectTasks(int projectId)
    {
        IEnumerable<Task> tasks;
        try
        {
            tasks = await _projectService.GetAllProjectTasks(projectId);
        }
        catch (ObjectNotFoundException exception)
        {
            return NotFound(exception.Data);
        }

        var mappedTasks = tasks.Select(p =>
            _mapper.Map<TaskModel>(p)
        );

        return Ok(mappedTasks);
    }

    [HttpGet]
    [Authorize(Roles = "admin, user")]
    [Route("project/{id:int}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        Project project;
        try
        {
            project = await _projectService.GetProjectById(id);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception);
        }

        var mappedProject = _mapper.Map<ProjectDataModel>(project);

        return Ok(mappedProject);
    }

    [HttpPost]
    [Authorize(Roles = "admin, user")]
    [Route("user/projects")]
    public async Task<IActionResult> PostNewUserProject(
        [FromBody] ProjectTaskGroupPostComposite data
    )
    {
        var mappedProject = _mapper.Map<Project>(data.ProjectPostModel);
        var mappedTaskGroup = _mapper.Map<TaskGroup>(data.TaskGroupProjectPostModel);
        mappedProject.TaskGroups.Add(mappedTaskGroup);

        Project? resultProject;
        try
        {
            resultProject = await _projectService.PostNewUserProject(mappedProject);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException exception)
        {
            var message = new
            {
                message = "Exception triggered on Db update",
                exception_message = exception.Message
            };

            return BadRequest(message);
        }
        var mappedResultProject = _mapper.Map<ProjectDataModel>(resultProject);

        return CreatedAtAction(
            nameof(GetProjectById),
            new { id = mappedResultProject.ProjectId },
            mappedResultProject
            );
    }

    [HttpPut]
    [Authorize(Roles = "admin, user")]
    [Route("project/{id:int}")]
    public async Task<IActionResult> PutProject(int id, [FromBody] ProjectDataModel project)
    {
        if (id != project.ProjectId)
        {
            return BadRequest();
        }

        var mappedProject = _mapper.Map<Project>(project);

        StatusCodeResult status;
        try
        {
            status = await _projectService.UpdateProject(id, mappedProject);
        }
        catch (DbUpdateConcurrencyException exception)
        {
            var message = new
            {
                message = "Exception triggered on Db update",
                exception_message = exception.Message
            };

            return BadRequest(message);
        }

        if (status.StatusCode == StatusCodes.Status204NoContent)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete]
    [Authorize(Roles = "admin, user")]
    [Route("project/{id:int}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var result = await _projectService.DeleteProject(id);

        if (result.StatusCode == StatusCodes.Status404NotFound)
        {
            return NotFound();
        }

        return NoContent();
    }
}