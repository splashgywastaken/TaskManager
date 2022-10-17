using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManager.Entities;

[Table("achievement")]
public class Achievement
{
    [Column("achievement_id")]
    [Key]
    [JsonIgnore]
    public int AchievementId { get; set;}

    [Column("achievement_name")]
    [JsonPropertyName("achievement_name")]
    public string AchievementName { get; set;}

    [Column("achievement_description")]
    [JsonPropertyName("achievement_description")]
    public string AchievementDescription { get; set;}

    [Column("achievement_points")] 
    [JsonPropertyName("achievement_points")]
    public int AchievementPoints { get; set; }

    [JsonIgnore]
    public ICollection<User> Users { get; set; }
    [JsonIgnore]
    public List<UsersAchievements> UsersAchievements { get; set; }
}