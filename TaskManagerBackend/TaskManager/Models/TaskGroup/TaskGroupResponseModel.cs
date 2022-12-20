using System.Text.Json.Serialization;
using TaskManager.Models.Task;

namespace TaskManager.Models.TaskGroup;

public class TaskGroupResponseModel
{
    [JsonPropertyName("task_group_id")]
    [JsonPropertyOrder(0)]
    public int TaskGroupId { get; set; }

    [JsonPropertyName("task_group_name")]
    [JsonPropertyOrder(2)]
    public string TaskGroupName { get; set; } = null!;

    [JsonPropertyName("task_group_description")]
    [JsonPropertyOrder(3)]
    public string TaskGroupDescription { get; set; } = null!;

    [JsonPropertyName("task_group_tasks")]
    [JsonPropertyOrder(4)]
    public List<TaskResponseModel> TaskGroupTasks { get; set; } = null!;
}