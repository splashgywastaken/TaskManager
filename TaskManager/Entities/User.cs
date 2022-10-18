using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TaskManager.Entities;

[Table("user")]
public class User
{
    [Column("user_id")]
    [Key]
    public int Id { get; set; }
    
    [Column("user_name")]
    public string UserName { get; set; }

    [Column("user_email")]
    public string UserEmail { get; set; }

    [Column("user_password")]
    public string UserPassword { get; set; }

    [Column("user_role")]
    public string UserRole { get; set; }

    [Column("user_achievements_score")]
    public int UserAchievementsScore { get; set; }

    // Projects
    [JsonIgnore]
    public List<Project> Projects { get; set; }

    // Achievements
    [JsonIgnore]
    public ICollection<Achievement> Achievements { get; set; }
    [JsonIgnore]
    public List<UsersAchievements> UsersAchievements { get; set; }
}