using System.Data.Entity.Core;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using TaskManager.Models.Project;
using TaskManager.Service.Entities.Project;

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
    [Route("{id:int}")]
    public async Task<IActionResult> GetProjectById([FromQuery]int id)
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
    public async Task<IActionResult> PutProject(int projectId, [FromBody] ProjectDataModel project)
    {
        if (projectId != project.ProjectId)
        {
            return BadRequest();
        }

        var mappedProject = _mapper.Map<Project>(project);

        var status = await _projectService.UpdateProject(projectId, mappedProject);

        if (status.StatusCode == StatusCodes.Status204NoContent)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteProject(int projectId)
    {
        var result = await _projectService.DeleteProject(projectId);

        if (result.StatusCode == StatusCodes.Status404NotFound)
        {
            return NotFound();
        }

        return NoContent();
    }
}