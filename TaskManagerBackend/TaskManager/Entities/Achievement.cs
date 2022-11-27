namespace TaskManager.Entities
{
    public partial class Achievement
    {
        public Achievement()
        {
            UsersAchievementsUsers = new HashSet<User>();
        }

        public int AchievementId { get; set; }
        public string AchievementName { get; set; } = null!;
        public string AchievementDescription { get; set; } = null!;
        public int AchievementPoints { get; set; }

        public virtual ICollection<User> UsersAchievementsUsers { get; set; }
        public List<UsersAchievements> UsersAchievements { get; set; }
    }
}
