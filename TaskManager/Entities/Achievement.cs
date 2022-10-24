using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManager.Entities;

[Table("achievement")]
public class Achievement
{
    [Column("achievement_id")]
    [Key]
    public int AchievementId { get; set;}

    [Column("achievement_name")]
    public string AchievementName { get; set;}

    [Column("achievement_description")]
    public string AchievementDescription { get; set;}

    [Column("achievement_points")] 
    public int AchievementPoints { get; set; }

    // Many-to-many related properties
    public ICollection<User>? Users { get; set; }
    public List<UsersAchievements>? UsersAchievements { get; set; }
}