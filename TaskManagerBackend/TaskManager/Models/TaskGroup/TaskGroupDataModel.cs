using System.Text.Json.Serialization;

namespace TaskManager.Models.TaskGroup;

public class TaskGroupDataModel
{
    [JsonPropertyName("task_group_id")]
    [JsonPropertyOrder(0)]
    public int TaskGroupId { get; set; }

    [JsonPropertyName("task_group_project_id")]
    [JsonPropertyOrder(1)]
    public int TaskGroupProjectId { get; set; }

    [JsonPropertyName("task_group_name")]
    [JsonPropertyOrder(2)]
    public string TaskGroupName { get; set; } = null!;

    [JsonPropertyName("task_group_description")]
    [JsonPropertyOrder(3)]
    public string TaskGroupDescription { get; set; } = null!;
}