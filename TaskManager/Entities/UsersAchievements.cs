namespace TaskManager.Entities
{
    public class UsersAchievements
    {
        public int UsersAchievementsAchievementId { get; set; }
        public Achievement Achievement { get; set; } = null!;
        public int UsersAchievementsUserId { get; set; }
        public User User { get; set; } = null!;
    }
}
