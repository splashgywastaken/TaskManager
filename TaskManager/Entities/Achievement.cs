namespace TaskManager.Entities
{
    public partial class Achievement
    {
        public Achievement()
        {
            Users = new HashSet<User>();
        }

        public int AchievementId { get; set; }
        public string AchievementName { get; set; } = null!;
        public string AchievementDescription { get; set; } = null!;
        public int AchievementPoints { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public List<UsersAchievement> UsersAchievements { get; set; }
    }
}
