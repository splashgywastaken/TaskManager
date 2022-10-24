namespace TaskManager.Service.Data.DbContext;

using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskGroup> TaskGroups { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TasksTags> TasksTags { get; set; }

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

        // One-to-many between task groups and tasks setup
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

        // Many-to-many between tasks and tags
        modelBuilder.Entity<Task>()
            .HasMany(p => p.Tags)
            .WithMany(p => p.Tasks)
            .UsingEntity<TasksTags>(
                    j => j
                        .HasOne(tasksTags => tasksTags.Tag)
                        .WithMany(tasks => tasks.TasksTags)
                        .HasForeignKey(tasksTags => tasksTags.TagId),
                    j => j
                        .HasOne(tasksTags => tasksTags.Task)
                        .WithMany(tags => tags.TaskTags)
                        .HasForeignKey(tasksTags => tasksTags.TaskId),
                    j =>
                    {
                        j.HasKey(tasksTags => new { tasksTags.TagId, tasksTags.TaskId });
                    }
                    );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TaskManagerDataBase"));
    }
}