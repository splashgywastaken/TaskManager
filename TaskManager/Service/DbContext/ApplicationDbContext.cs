namespace TaskManager.Service.DbContext;

using Microsoft.EntityFrameworkCore;
using Entities;

public class ApplicationDbContext : DbContext
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