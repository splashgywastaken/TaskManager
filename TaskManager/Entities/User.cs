using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Entities;

[Table("user")]
public class User
{
    [Column("user_id")]
    public int Id { get; set; }

    [Column("user_name")]
    public string UserName { get; set; }

    [Column("user_email")]
    public string UserEmail { get; set; }

    [Column("user_password")]
    public string UserPassword { get; set; }

    [Column("user_role")]
    public string UserRole { get; set; }

    [Column("user_achievments_score")]
    public int UserAchievementsScore { get; set; }
}