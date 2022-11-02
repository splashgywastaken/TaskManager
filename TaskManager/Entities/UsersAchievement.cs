namespace TaskManager.Entities
{
    public class UsersAchievement
    {
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
