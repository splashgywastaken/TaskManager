using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    public partial class InitialDBSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "achievement",
                columns: table => new
                {
                    achievement_id = table.Column<int>(type: "int", nullable: false),
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
                    tag_id = table.Column<int>(type: "int", nullable: false),
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
                    user_id = table.Column<int>(type: "int", nullable: false),
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
                    project_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_PROJECT_USER_PROJ_USER",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "users_achievements",
                columns: table => new
                {
                    achievement_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    AchievementId1 = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_achievements", x => new { x.user_id, x.achievement_id });
                    table.ForeignKey(
                        name: "FK_USERS_AC_USERS_ACH_ACHIEVME",
                        column: x => x.achievement_id,
                        principalTable: "achievement",
                        principalColumn: "achievement_id");
                    table.ForeignKey(
                        name: "FK_USERS_AC_USERS_ACH_USER",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_users_achievements_achievement_AchievementId1",
                        column: x => x.AchievementId1,
                        principalTable: "achievement",
                        principalColumn: "achievement_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_achievements_user_UserId1",
                        column: x => x.UserId1,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_group",
                columns: table => new
                {
                    task_group_id = table.Column<int>(type: "int", nullable: false),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    task_group_name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    task_group_description = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_group", x => x.task_group_id);
                    table.ForeignKey(
                        name: "FK_TASK_GRO_PROJECT_T_PROJECT",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id");
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "int", nullable: false),
                    task_group_id = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_TASK_TASK_GROU_TASK_GRO",
                        column: x => x.task_group_id,
                        principalTable: "task_group",
                        principalColumn: "task_group_id");
                });

            migrationBuilder.CreateTable(
                name: "TasksTags",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksTags", x => new { x.TagId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TASKS_TA_TASKS_TAG_TAG",
                        column: x => x.TagId,
                        principalTable: "tag",
                        principalColumn: "tag_id");
                    table.ForeignKey(
                        name: "FK_TASKS_TA_TASKS_TAG_TASK",
                        column: x => x.TaskId,
                        principalTable: "task",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateIndex(
                name: "user_projects_FK",
                table: "project",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "task_group_tasks_FK",
                table: "task",
                column: "task_group_id");

            migrationBuilder.CreateIndex(
                name: "project_task_groups_FK",
                table: "task_group",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_TasksTags_TaskId",
                table: "TasksTags",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_users_achievements_achievement_id",
                table: "users_achievements",
                column: "achievement_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_achievements_AchievementId1",
                table: "users_achievements",
                column: "AchievementId1");

            migrationBuilder.CreateIndex(
                name: "IX_users_achievements_UserId1",
                table: "users_achievements",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TasksTags");

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
