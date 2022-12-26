using System.Text.Json.Serialization;

namespace TaskManager.Models.Project
{
    public class ProjectDataModel
    {
        [JsonPropertyName("project_id")]
        public int ProjectId { get; set; }

        [JsonPropertyName("project_user_id")]
        public int ProjectUserId { get; set; }
        
        [JsonPropertyName("project_name")]
        public string ProjectName { get; set; } = null!;
        
        [JsonPropertyName("project_description")]
        public string? ProjectDescription { get; set; }

        [JsonPropertyName("project_start_date")]
        public DateTime ProjectStartDate { get; set; }

        [JsonPropertyName("project_finish_date")]
        public DateTime ProjectFinishDate { get; set; }

        [JsonPropertyName("project_completion_status")]
        public bool? ProjectCompletionStatus { get; set; }
    }
}
