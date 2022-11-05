using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "achievement",
                columns: table => new
                {
                    achievement_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    achievement_name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    achievement_description = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    achievement_points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_achievement", x => x.achievement_id);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    tag_description = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    user_email = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    user_password = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    user_role = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    user_achievements_score = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_user_id = table.Column<int>(type: "int", nullable: true),
                    project_name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    project_description = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    project_start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    project_finish_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    project_completion_status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_USER_PROJECTS",
                        column: x => x.project_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "users_achievements",
                columns: table => new
                {
                    users_achievements_achievement_id = table.Column<int>(type: "int", nullable: false),
                    users_achievements_user_id = table.Column<int>(type: "int", nullable: false),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_achievements", x => new { x.users_achievements_user_id, x.users_achievements_achievement_id });
                    table.ForeignKey(
                        name: "FK_USERS_ACHIEVEMENTS_ACHIEVEMENT",
                        column: x => x.users_achievements_achievement_id,
                        principalTable: "achievement",
                        principalColumn: "achievement_id");
                    table.ForeignKey(
                        name: "FK_users_achievements_achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "achievement",
                        principalColumn: "achievement_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERS_ACHIEVEMENTS_USER",
                        column: x => x.users_achievements_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_users_achievements_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_group",
                columns: table => new
                {
                    task_group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    task_group_project_id = table.Column<int>(type: "int", nullable: true),
                    task_group_name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    task_group_description = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_group", x => x.task_group_id);
                    table.ForeignKey(
                        name: "FK_PROJECT_TASK_GROUPS",
                        column: x => x.task_group_project_id,
                        principalTable: "project",
                        principalColumn: "project_id");
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    task_task_group_id = table.Column<int>(type: "int", nullable: true),
                    task_name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    task_description = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    task_start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    task_finish_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    task_completion_status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_TASK_TASK_GROUPS",
                        column: x => x.task_task_group_id,
                        principalTable: "task_group",
                        principalColumn: "task_group_id");
                });

            migrationBuilder.CreateTable(
                name: "tasks_tags",
                columns: table => new
                {
                    tasks_tags_task_id = table.Column<int>(type: "int", nullable: false),
                    tasks_tags_tag_id = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks_tags", x => new { x.tasks_tags_tag_id, x.tasks_tags_task_id });
                    table.ForeignKey(
                        name: "FK_TASKS_TA_TASKS_TAG_TAG",
                        column: x => x.tasks_tags_tag_id,
                        principalTable: "tag",
                        principalColumn: "tag_id");
                    table.ForeignKey(
                        name: "FK_TASKS_TA_TASKS_TAG_TASK",
                        column: x => x.tasks_tags_task_id,
                        principalTable: "task",
                        principalColumn: "task_id");
                    table.ForeignKey(
                        name: "FK_tasks_tags_tag_TagId",
                        column: x => x.TagId,
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tasks_tags_task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "task",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "user_projects_FK",
                table: "project",
                column: "project_user_id");

            migrationBuilder.CreateIndex(
                name: "task_group_tasks_FK",
                table: "task",
                column: "task_task_group_id");

            migrationBuilder.CreateIndex(
                name: "project_task_groups_FK",
                table: "task_group",
                column: "task_group_project_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_tags_TagId",
                table: "tasks_tags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_tags_TaskId",
                table: "tasks_tags",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_tags_tasks_tags_task_id",
                table: "tasks_tags",
                column: "tasks_tags_task_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_achievements_AchievementId",
                table: "users_achievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_users_achievements_UserId",
                table: "users_achievements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_achievements_users_achievements_achievement_id",
                table: "users_achievements",
                column: "users_achievements_achievement_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tasks_tags");

            migrationBuilder.DropTable(
                name: "users_achievements");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "achievement");

            migrationBuilder.DropTable(
                name: "task_group");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
