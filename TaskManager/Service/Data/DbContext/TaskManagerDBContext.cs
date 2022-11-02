using System.Configuration;
using System.Data.Entity.Migrations;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Service.Data.DbContext
{
    public partial class TaskManagerDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TaskManagerDBContext()
        {
        }

        public TaskManagerDBContext(IConfiguration configuration, DbContextOptions<TaskManagerDBContext> options)
            : base(options)
        {
            _configuration = configuration;

        }

        public virtual DbSet<Achievement> Achievements { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskGroup> TaskGroups { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsersAchievement> UsersAchievement { get; set; } = null!;

        private IConfiguration _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TaskManagerDataBase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.ToTable("achievement");

                entity.Property(e => e.AchievementId)
                    .ValueGeneratedNever()
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

                entity.HasIndex(e => e.UserId, "user_projects_FK");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
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

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PROJECT_USER_PROJ_USER");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.TagId)
                    .ValueGeneratedNever()
                    .HasColumnName("tag_id");

                entity.Property(e => e.TagDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("tag_description");

                entity.Property(e => e.TagName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("tag_name");

                entity.HasMany(d => d.Tasks)
                    .WithMany(p => p.Tags)
                    .UsingEntity<TasksTags>(
                        l => 
                            l.HasOne<Task>().WithMany().HasForeignKey("TaskId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TASKS_TA_TASKS_TAG_TASK"),
                        r => 
                            r.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TASKS_TA_TASKS_TAG_TAG")
                        );
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.HasIndex(e => e.TaskGroupId, "task_group_tasks_FK");

                entity.Property(e => e.TaskId)
                    .ValueGeneratedNever()
                    .HasColumnName("task_id");

                entity.Property(e => e.TaskCompletionStatus).HasColumnName("task_completion_status");

                entity.Property(e => e.TaskDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("task_description");

                entity.Property(e => e.TaskFinishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("task_finish_date");

                entity.Property(e => e.TaskGroupId).HasColumnName("task_group_id");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("task_name");

                entity.Property(e => e.TaskStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("task_start_date");

                entity.HasOne(d => d.TaskGroup)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskGroupId)
                    .HasConstraintName("FK_TASK_TASK_GROU_TASK_GRO");
            });

            modelBuilder.Entity<UsersAchievement>(entity =>
            {
                entity.ToTable("users_achievements");

                entity.HasKey(p => new {p.UserId, p.AchievementId});

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.AchievementId)
                    .ValueGeneratedNever()
                    .HasColumnName("achievement_id");
            });

            modelBuilder.Entity<TaskGroup>(entity =>
            {
                entity.ToTable("task_group");

                entity.HasIndex(e => e.ProjectId, "project_task_groups_FK");

                entity.Property(e => e.TaskGroupId)
                    .ValueGeneratedNever()
                    .HasColumnName("task_group_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.TaskGroupDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("task_group_description");

                entity.Property(e => e.TaskGroupName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("task_group_name");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TaskGroups)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_TASK_GRO_PROJECT_T_PROJECT");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
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

                entity.HasMany(d => d.Achievements)
                    .WithMany(p => p.Users)
                    .UsingEntity<UsersAchievement>(
                        l => 
                            l.HasOne<Achievement>().WithMany().HasForeignKey("AchievementId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_USERS_AC_USERS_ACH_ACHIEVME"),
                        r => 
                            r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_USERS_AC_USERS_ACH_USER")
                        );
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
