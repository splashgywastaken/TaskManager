using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using TaskManager.Models.TaskGroup;
using TaskManager.Service.Entities.TaskGroup;

namespace TaskManager.Controllers;

[Controller]
public class TaskGroupController : Controller
{
    private readonly ITaskGroupService _taskGroupService;
    private readonly IMapper _mapper;

    public TaskGroupController(ITaskGroupService taskGroupService, IMapper mapper)
    {
        _taskGroupService = taskGroupService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("taskGroups/{taskGroupId:int}")]
    public async Task<IActionResult> GetTaskGroupById(int taskGroupId)
    {
        TaskGroup taskGroup;
        try
        {
            taskGroup = await _taskGroupService.GetById(taskGroupId);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception);
        }

        var mappedTaskGroup = _mapper.Map<TaskGroupDataModel>(taskGroup);

        return Ok(mappedTaskGroup);
    }

    [HttpPost]
    [Route("project/{projectId}/taskGroup")]
    public async Task<IActionResult> PostNewTaskGroup(int projectId, [FromBody] TaskGroupDataModel data)
    {
        var mappedTaskGroup = _mapper.Map<TaskGroup>(data);
        mappedTaskGroup.TaskGroupProjectId = projectId;

        TaskGroup taskGroup;
        try
        {
            taskGroup = await _taskGroupService.PostTaskGroup(mappedTaskGroup);
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
        var mappedResultTaskGroup = _mapper.Map<TaskGroupDataModel>(taskGroup);

        return CreatedAtAction(
            nameof(GetTaskGroupById),
            new { taskGroupId = mappedResultTaskGroup.TaskGroupId },
            mappedResultTaskGroup
        );
    }

    [HttpPut]
    [Route("project/{projectId:int}/taskGroup/{taskGroupId:int}")]
    public async Task<IActionResult> PutTaskGroup(
        int projectId,
        int taskGroupId,
        [FromBody] TaskGroupDataModel data
        )
    {
        if (taskGroupId != data.TaskGroupId || projectId != data.TaskGroupProjectId)
        {
            return BadRequest();
        }

        var mappedTaskGroup = _mapper.Map<TaskGroup>(data);

        StatusCodeResult status;
        try
        {
            status = await _taskGroupService.UpdateTaskGroup(taskGroupId, mappedTaskGroup);
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
    [Route("taskGroup/{taskGroupId:int}")]
    public async Task<IActionResult> DeleteTaskGroup(int taskGroupId)
    {
        var result = await _taskGroupService.DeleteTaskGroup(taskGroupId);

        if (result.StatusCode == StatusCodes.Status404NotFound)
        {
            return NotFound();
        }

        return NoContent();
    }
}