using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaskManagerWPF.Model.User
{
    public class UserDataModel
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("user_role")]
        public string UserRole { get; set; } = null!;
        [JsonProperty("user_name")]
        public string UserName { get; set; } = null!;
        [JsonProperty("user_email")]
        public string UserEmail { get; set; } = null!;
        [JsonProperty("user_achievements_score")]
        public int UserAchievementsScore { get; set; }
    }
}
