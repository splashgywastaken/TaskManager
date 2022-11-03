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
    [Migration("20221102162838_InitialDBSetup")]
    partial class InitialDBSetup
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
                        .HasColumnType("int")
                        .HasColumnName("achievement_id");

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

            modelBuilder.Entity("TaskManager.Entities.ProjectDataModel", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

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

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("ProjectId");

                    b.HasIndex(new[] { "UserId" }, "user_projects_FK");

                    b.ToTable("project", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int")
                        .HasColumnName("tag_id");

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
                        .HasColumnType("int")
                        .HasColumnName("task_id");

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

                    b.Property<int?>("TaskGroupId")
                        .HasColumnType("int")
                        .HasColumnName("task_group_id");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("task_name");

                    b.Property<DateTime>("TaskStartDate")
                        .HasColumnType("datetime")
                        .HasColumnName("task_start_date");

                    b.HasKey("TaskId");

                    b.HasIndex(new[] { "TaskGroupId" }, "task_group_tasks_FK");

                    b.ToTable("task", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.TaskGroup", b =>
                {
                    b.Property<int>("TaskGroupId")
                        .HasColumnType("int")
                        .HasColumnName("task_group_id");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

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

                    b.HasKey("TaskGroupId");

                    b.HasIndex(new[] { "ProjectId" }, "project_task_groups_FK");

                    b.ToTable("task_group", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.TasksTags", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("TagId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("TasksTags");
                });

            modelBuilder.Entity("TaskManager.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

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

            modelBuilder.Entity("TaskManager.Entities.UsersAchievement", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("AchievementId")
                        .HasColumnType("int")
                        .HasColumnName("achievement_id");

                    b.Property<int>("AchievementId1")
                        .HasColumnType("int");

                    b.Property<int>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("UserId", "AchievementId");

                    b.HasIndex("AchievementId");

                    b.HasIndex("AchievementId1");

                    b.HasIndex("UserId1");

                    b.ToTable("users_achievements", (string)null);
                });

            modelBuilder.Entity("TaskManager.Entities.ProjectDataModel", b =>
                {
                    b.HasOne("TaskManager.Entities.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_PROJECT_USER_PROJ_USER");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManager.Entities.Task", b =>
                {
                    b.HasOne("TaskManager.Entities.TaskGroup", "TaskGroup")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskGroupId")
                        .HasConstraintName("FK_TASK_TASK_GROU_TASK_GRO");

                    b.Navigation("TaskGroup");
                });

            modelBuilder.Entity("TaskManager.Entities.TaskGroup", b =>
                {
                    b.HasOne("TaskManager.Entities.ProjectDataModel", "ProjectDataModel")
                        .WithMany("TaskGroups")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_TASK_GRO_PROJECT_T_PROJECT");

                    b.Navigation("ProjectDataModel");
                });

            modelBuilder.Entity("TaskManager.Entities.TasksTags", b =>
                {
                    b.HasOne("TaskManager.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .IsRequired()
                        .HasConstraintName("FK_TASKS_TA_TASKS_TAG_TAG");

                    b.HasOne("TaskManager.Entities.Task", null)
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .IsRequired()
                        .HasConstraintName("FK_TASKS_TA_TASKS_TAG_TASK");
                });

            modelBuilder.Entity("TaskManager.Entities.UsersAchievement", b =>
                {
                    b.HasOne("TaskManager.Entities.Achievement", null)
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .IsRequired()
                        .HasConstraintName("FK_USERS_AC_USERS_ACH_ACHIEVME");

                    b.HasOne("TaskManager.Entities.Achievement", "Achievement")
                        .WithMany("UsersAchievements")
                        .HasForeignKey("AchievementId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_USERS_AC_USERS_ACH_USER");

                    b.HasOne("TaskManager.Entities.User", "User")
                        .WithMany("UsersAchievements")
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManager.Entities.Achievement", b =>
                {
                    b.Navigation("UsersAchievements");
                });

            modelBuilder.Entity("TaskManager.Entities.ProjectDataModel", b =>
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
