using System.Data.Entity.Core;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.Tag;
using TaskManager.Models.Task;
using TaskManager.Service.Entities.Task;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController : Controller
{
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;

    public TaskController(ITaskService taskService, IMapper mapper)
    {
        _taskService = taskService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{id:int}/tags")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetAllTaskTags(int taskId)
    {
        IEnumerable<Tag> tags;
        try
        {
            tags = await _taskService.GetAllTaskTags(taskId);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Data);
        }

        var mappedTags = tags.Select(tag =>
                _mapper.Map<TagDataModel>(tag)
            );

        return Ok(mappedTags);
    }

    [HttpGet]
    [Route("{id:int}")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetTask(int id)
    {
        Task task;
        try
        {
            task = await _taskService.GetTaskById(id);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Data);
        }

        var mappedTask = _mapper.Map<TaskModel>(task);

        return Ok(mappedTask);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskModel task)
    {
        if (id != task.TaskId)
        {
            return BadRequest();
        }

        var mappedTask = _mapper.Map<Task>(task);

        var status = await _taskService.PutTask(id, mappedTask);

        if (status.StatusCode == StatusCodes.Status204NoContent)
        {
            return NoContent();
        }

        return NotFound();
    }
}