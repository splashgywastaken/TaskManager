using System.Text.Json.Serialization;

namespace TaskManager.Models.Task
{
    public class TaskModel
    {
        [JsonPropertyName("task_id")]
        [JsonPropertyOrder(0)]
        public int TaskId { get; set; }

        [JsonPropertyName("task_name")]
        [JsonPropertyOrder(1)]
        public string TaskName { get; set; } = null!;

        [JsonPropertyName("task_description")]
        [JsonPropertyOrder(2)]
        public string TaskDescription { get; set; } = null!;

        [JsonPropertyName("task_completion_status")]
        [JsonPropertyOrder(3)]
        public bool TaskCompletionStatus { get; set; }

        [JsonPropertyName("task_start_date")]
        [JsonPropertyOrder(4)]
        public DateTime TaskStartDate { get; set; }

        [JsonPropertyName("task_finish_date")]
        [JsonPropertyOrder(5)]
        public DateTime TaskFinishDate { get; set; }
    }
}
