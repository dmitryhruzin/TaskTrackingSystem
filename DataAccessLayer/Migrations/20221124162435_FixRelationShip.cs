using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class FixRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserProjects_ProjectId_TaskId_UserId",
                table: "UserProjects");

            migrationBuilder.DropIndex(
                name: "IX_UserProjects_TaskId",
                table: "UserProjects");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "UserProjects");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProjectStatuses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AssignmentStatuses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserProjects_TaskId_UserId",
                table: "UserProjects",
                columns: new[] { "TaskId", "UserId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProjectStatuses_Name",
                table: "ProjectStatuses",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AssignmentStatuses_Name",
                table: "AssignmentStatuses",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ProjectId",
                table: "Assignments",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Projects_ProjectId",
                table: "Assignments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Projects_ProjectId",
                table: "Assignments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserProjects_TaskId_UserId",
                table: "UserProjects");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProjectStatuses_Name",
                table: "ProjectStatuses");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AssignmentStatuses_Name",
                table: "AssignmentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ProjectId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "UserProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProjectStatuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AssignmentStatuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserProjects_ProjectId_TaskId_UserId",
                table: "UserProjects",
                columns: new[] { "ProjectId", "TaskId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_TaskId",
                table: "UserProjects",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
