using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManager.Models.User;

public class UserDataModel
{
    [JsonPropertyOrder(0)]
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    [JsonPropertyOrder(1)]
    [JsonPropertyName("user_name")]
    public string UserName { get; set; } = null!;

    [JsonPropertyOrder(2)]
    [JsonPropertyName("user_email")]
    [RegularExpression(@"(\S{1,})@(\S{1,}).(\S{1,})")]
    public string UserEmail { get; set; } = null!;

    [JsonPropertyOrder(3)]
    [JsonPropertyName("user_achievements_score")]
    public int UserAchievementsScore { get; set; }

    [JsonPropertyOrder(4)]
    [JsonPropertyName("user_role")]
    public string UserRole { get; set; } = null!;
}