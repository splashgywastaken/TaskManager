﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.Service.Data.DbContext;

#nullable disable

namespace TaskManager.Migrations
{
    [DbContext(typeof(TaskManagerDBContext))]
    [Migration("20221105215054_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TaskManager.Entities.Achievement", b =>
                {
                    b.Property<int>("AchievementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("achievement_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AchievementId"), 1L, 1);

                    b.Property<string>("AchievementDescription")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("achievement_description");

                    b.Property<string>("AchievementName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("achievement_name");

                    b.Property<int>("AchievementPoints")
                        .HasColumnType("int")
                        .HasColumnName("achievement_points");

                    b.HasKey("AchievementId");

                    b.ToTable("achievement", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"), 1L, 1);

                    b.Property<bool?>("ProjectCompletionStatus")
                        .HasColumnType("bit")
                        .HasColumnName("project_completion_status");

                    b.Property<string>("ProjectDescription")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("project_description");

                    b.Property<DateTime>("ProjectFinishDate")
                        .HasColumnType("datetime")
                        .HasColumnName("project_finish_date");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("project_name");

                    b.Property<DateTime>("ProjectStartDate")
                        .HasColumnType("datetime")
                        .HasColumnName("project_start_date");

                    b.Property<int?>("ProjectUserId")
                        .HasColumnType("int")
                        .HasColumnName("project_user_id");

                    b.HasKey("ProjectId");

                    b.HasIndex(new[] { "ProjectUserId" }, "user_projects_FK");

                    b.ToTable("project", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("tag_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"), 1L, 1);

                    b.Property<string>("TagDescription")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("tag_description");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("tag_name");

                    b.HasKey("TagId");

                    b.ToTable("tag", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("task_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"), 1L, 1);

                    b.Property<bool>("TaskCompletionStatus")
                        .HasColumnType("bit")
                        .HasColumnName("task_completion_status");

                    b.Property<string>("TaskDescription")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("task_description");

                    b.Property<DateTime>("TaskFinishDate")
                        .HasColumnType("datetime")
                        .HasColumnName("task_finish_date");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("task_name");

                    b.Property<DateTime>("TaskStartDate")
                        .HasColumnType("datetime")
                        .HasColumnName("task_start_date");

                    b.Property<int?>("TaskTaskGroupId")
                        .HasColumnType("int")
                        .HasColumnName("task_task_group_id");

                    b.HasKey("TaskId");

                    b.HasIndex(new[] { "TaskTaskGroupId" }, "task_group_tasks_FK");

                    b.ToTable("task", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.TaskGroup", b =>
                {
                    b.Property<int>("TaskGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("task_group_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskGroupId"), 1L, 1);

                    b.Property<string>("TaskGroupDescription")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("task_group_description");

                    b.Property<string>("TaskGroupName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("task_group_name");

                    b.Property<int?>("TaskGroupProjectId")
                        .HasColumnType("int")
                        .HasColumnName("task_group_project_id");

                    b.HasKey("TaskGroupId");

                    b.HasIndex(new[] { "TaskGroupProjectId" }, "project_task_groups_FK");

                    b.ToTable("task_group", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.TasksTags", b =>
                {
                    b.Property<int>("TasksTagsTagId")
                        .HasColumnType("int")
                        .HasColumnName("tasks_tags_tag_id");

                    b.Property<int>("TasksTagsTaskId")
                        .HasColumnType("int")
                        .HasColumnName("tasks_tags_task_id");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("TasksTagsTagId", "TasksTagsTaskId");

                    b.HasIndex("TagId");

                    b.HasIndex("TaskId");

                    b.HasIndex("TasksTagsTaskId");

                    b.ToTable("tasks_tags", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int?>("UserAchievementsScore")
                        .HasColumnType("int")
                        .HasColumnName("user_achievements_score");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("user_email");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("user_name");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("user_password");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("user_role");

                    b.HasKey("UserId");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.UsersAchievements", b =>
                {
                    b.Property<int>("UsersAchievementsUserId")
                        .HasColumnType("int")
                        .HasColumnName("users_achievements_user_id");

                    b.Property<int>("UsersAchievementsAchievementId")
                        .HasColumnType("int")
                        .HasColumnName("users_achievements_achievement_id");

                    b.Property<int>("AchievementId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UsersAchievementsUserId", "UsersAchievementsAchievementId");

                    b.HasIndex("AchievementId");

                    b.HasIndex("UserId");

                    b.HasIndex("UsersAchievementsAchievementId");

                    b.ToTable("users_achievements", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.Project", b =>
                {
                    b.HasOne("TaskManager.Entities.User", "ProjectUser")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectUserId")
                        .HasConstraintName("FK_USER_PROJECTS");

                    b.Navigation("ProjectUser");
                });

            modelBuilder.Entity("TaskManager.Entities.Task", b =>
                {
                    b.HasOne("TaskManager.Entities.TaskGroup", "TaskTaskGroup")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskTaskGroupId")
                        .HasConstraintName("FK_TASK_TASK_GROUPS");

                    b.Navigation("TaskTaskGroup");
                });

            modelBuilder.Entity("TaskManager.Entities.TaskGroup", b =>
                {
                    b.HasOne("TaskManager.Entities.Project", "TaskGroupProject")
                        .WithMany("TaskGroups")
                        .HasForeignKey("TaskGroupProjectId")
                        .HasConstraintName("FK_PROJECT_TASK_GROUPS");

                    b.Navigation("TaskGroupProject");
                });

            modelBuilder.Entity("TaskManager.Entities.TasksTags", b =>
                {
                    b.HasOne("TaskManager.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Entities.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TasksTagsTagId")
                        .IsRequired()
                        .HasConstraintName("FK_TASKS_TA_TASKS_TAG_TAG");

                    b.HasOne("TaskManager.Entities.Task", null)
                        .WithMany()
                        .HasForeignKey("TasksTagsTaskId")
                        .IsRequired()
                        .HasConstraintName("FK_TASKS_TA_TASKS_TAG_TASK");

                    b.Navigation("Tag");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManager.Entities.UsersAchievements", b =>
                {
                    b.HasOne("TaskManager.Entities.Achievement", "Achievement")
                        .WithMany("UsersAchievements")
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Entities.User", "User")
                        .WithMany("UsersAchievements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Entities.Achievement", null)
                        .WithMany()
                        .HasForeignKey("UsersAchievementsAchievementId")
                        .IsRequired()
                        .HasConstraintName("FK_USERS_ACHIEVEMENTS_ACHIEVEMENT");

                    b.HasOne("TaskManager.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersAchievementsUserId")
                        .IsRequired()
                        .HasConstraintName("FK_USERS_ACHIEVEMENTS_USER");

                    b.Navigation("Achievement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManager.Entities.Achievement", b =>
                {
                    b.Navigation("UsersAchievements");
                });

            modelBuilder.Entity("TaskManager.Entities.Project", b =>
                {
                    b.Navigation("TaskGroups");
                });

            modelBuilder.Entity("TaskManager.Entities.TaskGroup", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManager.Entities.User", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("UsersAchievements");
                });
#pragma warning restore 612, 618
        }
    }
}