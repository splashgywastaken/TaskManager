using Newtonsoft.Json;

namespace TaskManagerWPF.Model.User;

public class UserWithAllData
{
    public UserWithAllData(string userName)
    {
        UserName = userName;
    }

    public UserWithAllData() { }

    [JsonProperty("user_id")]
    public int UserId { get; set; }
    [JsonProperty("user_name")]
    public string UserName { get; set; } = null!;
    [JsonProperty("user_email")]
    public string UserEmail { get; set; } = null!;
    [JsonProperty("user_achievements_score")]
    public int UserAchievementsScore { get; set; }

    [JsonProperty("user_role")] 
    public string UserRole { get; set; } = null!;
}