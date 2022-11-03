using System.Text.Json.Serialization;
using TaskManager.Models.Project;

namespace TaskManager.Models.User
{
    public class UserProjectsModel
    {
        [JsonPropertyOrder(0)]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [JsonPropertyOrder(1)]
        [JsonPropertyName("user_email")]
        public string UserEmail { get; set; }

        [JsonPropertyOrder(2)]
        [JsonPropertyName("user_projects")]
        public ICollection<ProjectDataModel> UserProjects { get; set; }
    }
}
