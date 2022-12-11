using System.Text.Json.Serialization;

namespace TaskManager.Models.Achievement;

public class AchievementModel
{
    [JsonPropertyOrder(0)]
    [JsonPropertyName("achievement_id")]
    public int AchievementId { get; set; }

    [JsonPropertyOrder(1)]
    [JsonPropertyName("achievement_name")]
    public string AchievementName { get; set; } = null!;

    [JsonPropertyOrder(2)]
    [JsonPropertyName("achievement_description")]
    public string AchievementDescription { get; set; } = null!;

    [JsonPropertyOrder(3)]
    [JsonPropertyName("achievement_points")]
    public int AchievementPoints { get; set; }
}