using System.Text.Json.Serialization;

namespace TaskManager.Models.Task;

public class TaskPostModel
{
    [JsonPropertyName("task_id")]
    public int TaskId { get; set; }
    [JsonPropertyName("task_task_group_id")]
    public int? TaskTaskGroupId { get; set; }
    [JsonPropertyName("task_name")]
    public string TaskName { get; set; } = null!;
    [JsonPropertyName("task_description")]
    public string? TaskDescription { get; set; }
    [JsonPropertyName("task_start_date")]
    public DateTime TaskStartDate { get; set; }
    [JsonPropertyName("task_finish_date")]
    public DateTime TaskFinishDate { get; set; }
    [JsonPropertyName("task_completion_status")]
    public bool TaskCompletionStatus { get; set; }
}