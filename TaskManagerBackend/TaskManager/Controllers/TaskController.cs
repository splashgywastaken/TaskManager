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
    [Route("task/{taskId:int}/tags")]
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
    [Route("tasks/{taskId:int}")]
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

    [HttpPost]
    [Route("taskGroup/{taskGroupId}/task")]
    public async Task<IActionResult> PostNewTask(int taskGroupId, [FromBody] TaskPostModel data)
    {
        if (taskGroupId != data.TaskTaskGroupId) return BadRequest();

        var mappedTask = _mapper.Map<Task>(data);

        Task task;
        try
        {
            task = await _taskService.PostTask(mappedTask);
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

        var mappedResult = _mapper.Map<TaskPostModel>(task);

        return CreatedAtAction(
            nameof(GetTask),
            new { id = mappedResult.TaskId},
            mappedResult
            );
    }

    [HttpPut]
    [Route("taskGroup/{taskGroupId:int}/task/{taskId:int}")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> UpdateTask(int taskGroupId, int taskId, [FromBody] TaskModel task)
    {
        if (taskId != task.TaskId)
        {
            return BadRequest();
        }

        var mappedTask = _mapper.Map<Task>(task);
        mappedTask.TaskTaskGroupId = taskGroupId;

        var status = await _taskService.PutTask(taskId, mappedTask);

        if (status.StatusCode == StatusCodes.Status204NoContent)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete]
    [Route("tasks/{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        var result = await _taskService.DeleteTask(taskId);

        if (result.StatusCode == StatusCodes.Status404NotFound)
        {
            return NotFound();
        }

        return NoContent();
    }
}