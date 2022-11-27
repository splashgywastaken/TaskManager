using System.Text.Json.Serialization;

namespace TaskManager.Models.TaskGroup
{
    public class TaskGroupProjectPostModel
    {
        [JsonPropertyOrder(1)]
        [JsonPropertyName("task_group_name")]
        public string TaskGroupName { get; set; } = null!;

        [JsonPropertyOrder(2)]
        [JsonPropertyName("task_group_description")]
        public string TaskGroupDescription { get; set; } = null!;
    }
}
