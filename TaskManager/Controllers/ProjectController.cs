using System.Data.Entity.Core;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using TaskManager.Models.Project;
using TaskManager.Models.Task;
using TaskManager.Service.Entities.Project;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Controllers;

[ApiController]
[Route("project")]
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
    [Route("all")]
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
    [Route("{projectId:int}/tasks")]
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
    [Route("{id:int}")]
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

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> PutProject(int id, [FromBody] ProjectDataModel project)
    {
        if (id != project.ProjectId)
        {
            return BadRequest();
        }

        var mappedProject = _mapper.Map<Project>(project);

        var status = await _projectService.UpdateProject(id, mappedProject);

        if (status.StatusCode == StatusCodes.Status204NoContent)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
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