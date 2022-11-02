using System.Text.Json.Serialization;
using TaskManager.Models.Achievement;

namespace TaskManager.Models.User;

public class UserAchievementsModel
{
    [JsonPropertyName("user_name")]
    [JsonPropertyOrder(0)]
    public string UserName { get; set; }

    [JsonPropertyName("user_email")]
    [JsonPropertyOrder(1)]
    public string UserEmail { get; set; }

    [JsonPropertyName("user_achievements_score")]
    [JsonPropertyOrder(2)]
    public int UserAchievementsScore { get; set; }

    [JsonPropertyName("user_achievements")]
    [JsonPropertyOrder(3)]
    public ICollection<AchievementModel> UserAchievements { get; set; }
}