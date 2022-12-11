namespace TaskManager.Entities
{
    public partial class User
    {
        public User()
        {
            Projects = new HashSet<Project>();
            UsersAchievementsAchievements = new HashSet<Achievement>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserRole { get; set; } = null!;
        public int? UserAchievementsScore { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<Achievement> UsersAchievementsAchievements { get; set; }
        public List<UsersAchievements> UsersAchievements { get; set; } = null!;
    }
}
