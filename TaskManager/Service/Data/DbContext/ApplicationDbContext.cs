namespace TaskManager.Service.Data.DbContext;

using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Achievement> Achievements { get; set; }

    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-many between user and projects setup
        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)
            .WithMany(p => p.Projects)
            .HasForeignKey(p => p.UserId);

        // One-to-many between projects and task groups setup
        modelBuilder.Entity<TaskGroup>()
            .HasOne(p => p.Project)
            .WithMany(p => p.TaskGroups)
            .HasForeignKey(p => p.ProjectId);

        // One-to-many between tasks and task groups setup
        modelBuilder.Entity<Task>()
            .HasOne(p => p.TaskGroup)
            .WithMany(p => p.Tasks)
            .HasForeignKey(p => p.TaskGroupId);

        // Many-to-many between achievements and user setup
        modelBuilder.Entity<User>()
            .HasMany(p => p.Achievements)
            .WithMany(p => p.Users)
            .UsingEntity<UsersAchievements>(
                j => j
                    // Achievement to user
                    .HasOne(ua => ua.Achievement)
                    .WithMany(a => a.UsersAchievements)
                    .HasForeignKey(ua => ua.UserId),
                j=> j
                    // User to achievements
                    .HasOne(ua => ua.User)
                    .WithMany(u => u.UsersAchievements)
                    .HasForeignKey(ua => ua.AchievementId),
                j =>
                {
                    j.HasKey(ua => new { ua.AchievementId, ua.UserId });
                }
            );

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TaskManagerDataBase"));
    }
}