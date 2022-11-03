using System.Text.Json.Serialization;

namespace TaskManager.Models.Project
{
    public class ProjectDataModel
    {
        [JsonPropertyOrder(0)]
        [JsonPropertyName("project_id")]
        public int ProjectId { get; set; }

        [JsonPropertyOrder(1)]
        [JsonPropertyName("project_name")]
        public string ProjectName { get; set; } = null!;

        [JsonPropertyOrder(2)]
        [JsonPropertyName("project_description")]
        public string? ProjectDescription { get; set; }

        [JsonPropertyOrder(3)]
        [JsonPropertyName("project_start_date")]
        public DateTime ProjectStartDate { get; set; }

        [JsonPropertyOrder(4)]
        [JsonPropertyName("project_finish_date")]
        public DateTime ProjectFinishDate { get; set; }

        [JsonPropertyOrder(5)]
        [JsonPropertyName("project_completion_status")]
        public bool? ProjectCompletionStatus { get; set; }
    }
}
