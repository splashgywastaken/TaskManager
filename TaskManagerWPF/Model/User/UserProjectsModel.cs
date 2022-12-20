using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaskManagerWPF.Model.User
{
    class UserProjectsModel
    {
        [JsonProperty("user_name")]
        public string UserName { get; set; } = null!;
        [JsonProperty("user_email")]
        public string UserEmail { get; set; } = null!;
        [JsonProperty("user_projects")]
        public List<Project.Project> UserProjects { get; set; } = null!;
    }
}
