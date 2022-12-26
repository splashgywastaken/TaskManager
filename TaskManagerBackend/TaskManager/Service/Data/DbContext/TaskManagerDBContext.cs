using System.Configuration;
using System.Data.Entity.Migrations;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Service.Data.DbContext
{
    public partial class TaskManagerDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TaskManagerDBContext() { }

        public TaskManagerDBContext(IConfiguration configuration, DbContextOptions<TaskManagerDBContext> options)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Achievement> Achievements { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TasksTags> TasksTags { get; set; } = null!;
        public virtual DbSet<TaskGroup> TaskGroups { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsersAchievements> UsersAchievement { get; set; } = null!;

        private readonly IConfiguration? _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration!.GetConnectionString("TaskManagerDataBase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.ToTable("achievement");

                entity.Property(e => e.AchievementId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1)
                    .HasColumnName("achievement_id");

                entity.Property(e => e.AchievementDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("achievement_description");

                entity.Property(e => e.AchievementName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("achievement_name");

                entity.Property(e => e.AchievementPoints).HasColumnName("achievement_points");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");

                entity.HasIndex(e => e.ProjectUserId, "user_projects_FK");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1)
                    .HasColumnName("project_id");

                entity.Property(e => e.ProjectCompletionStatus).HasColumnName("project_completion_status");

                entity.Property(e => e.ProjectDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("project_description");

                entity.Property(e => e.ProjectFinishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("project_finish_date");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("project_name");

                entity.Property(e => e.ProjectStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("project_start_date");

                entity.Property(e => e.ProjectUserId).HasColumnName("project_user_id");

                entity.HasOne(d => d.ProjectUser)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectUserId)
                    .HasConstraintName("FK_USER_PROJECTS");
            });

            modelBuilder.Entity<TasksTags>(entity =>
            {
                entity.ToTable("tasks_tags");

                entity.HasKey(p => new { p.TasksTagsTagId, p.TasksTagsTaskId });

                entity.Property(e => e.TasksTagsTagId)
                    .ValueGeneratedNever()
                    .HasColumnName("tasks_tags_tag_id");

                entity.Property(e => e.TasksTagsTaskId)
                    .ValueGeneratedNever()
                    .HasColumnName("tasks_tags_task_id");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.TagId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1)
                    .HasColumnName("tag_id");

                entity.Property(e => e.TagDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("tag_description");

                entity.Property(e => e.TagName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("tag_name");

                entity.HasMany(d => d.TasksTagsTasks)
                    .WithMany(p => p.TasksTagsTags)
                    .UsingEntity<TasksTags>(
                        l =>
                            l.HasOne<Task>().WithMany().HasForeignKey("TasksTagsTaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TASKS_TA_TASKS_TAG_TASK"),
                        r =>
                            r.HasOne<Tag>().WithMany().HasForeignKey("TasksTagsTagId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TASKS_TA_TASKS_TAG_TAG")
                    );
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.HasIndex(e => e.TaskTaskGroupId, "task_group_tasks_FK");

                entity.Property(e => e.TaskId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1)
                    .HasColumnName("task_id");

                entity.Property(e => e.TaskCompletionStatus).HasColumnName("task_completion_status");

                entity.Property(e => e.TaskDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("task_description");

                entity.Property(e => e.TaskFinishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("task_finish_date");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("task_name");

                entity.Property(e => e.TaskStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("task_start_date");

                entity.Property(e => e.TaskTaskGroupId).HasColumnName("task_task_group_id");

                entity.HasOne(d => d.TaskTaskGroup)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskTaskGroupId)
                    .HasConstraintName("FK_TASK_TASK_GROUPS");
            });

            modelBuilder.Entity<TaskGroup>(entity =>
            {
                entity.ToTable("task_group");

                entity.HasIndex(e => e.TaskGroupProjectId, "project_task_groups_FK");

                entity.Property(e => e.TaskGroupId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1)
                    .HasColumnName("task_group_id");

                entity.Property(e => e.TaskGroupDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("task_group_description");

                entity.Property(e => e.TaskGroupName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("task_group_name");

                entity.Property(e => e.TaskGroupProjectId).HasColumnName("task_group_project_id");

                entity.HasOne(d => d.TaskGroupProject)
                    .WithMany(p => p.TaskGroups)
                    .HasForeignKey(d => d.TaskGroupProjectId)
                    .HasConstraintName("FK_PROJECT_TASK_GROUPS");
            });

            modelBuilder.Entity<UsersAchievements>(entity =>
            {
                entity.ToTable("users_achievements");

                entity.HasKey(p => new { p.UsersAchievementsUserId, p.UsersAchievementsAchievementId });

                entity.Property(e => e.UsersAchievementsUserId)
                    .ValueGeneratedNever()
                    .HasColumnName("users_achievements_user_id");

                entity.Property(e => e.UsersAchievementsAchievementId)
                    .ValueGeneratedNever()
                    .HasColumnName("users_achievements_achievement_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1)
                    .HasColumnName("user_id");

                entity.Property(e => e.UserAchievementsScore).HasColumnName("user_achievements_score");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserRole)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("user_role");

                entity.HasMany(d => d.UsersAchievementsAchievements)
                    .WithMany(p => p.UsersAchievementsUsers)
                    .UsingEntity<UsersAchievements>(
                        l =>
                            l.HasOne<Achievement>()
                                .WithMany()
                                .HasForeignKey(p => p.UsersAchievementsAchievementId)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_USERS_ACHIEVEMENTS_ACHIEVEMENT"),
                        r =>
                            r.HasOne<User>()
                                .WithMany()
                                .HasForeignKey(p => p.UsersAchievementsUserId)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_USERS_ACHIEVEMENTS_USER")
                    );
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
