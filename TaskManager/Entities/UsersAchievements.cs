using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Entities;

[Table("users_achievements")]
public class UsersAchievements
{
    [Column("user_id")]
    public int UserId { get; set; }
    public User User { get; set; }

    [Column("achievement_id")]
    public int AchievementId { get; set; }
    public Achievement Achievement { get; set; }
}