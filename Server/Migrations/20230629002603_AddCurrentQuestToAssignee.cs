using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestGiver.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentQuestToAssignee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Assignees_AssigneeId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_AssigneeId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Quests");

            migrationBuilder.AddColumn<int>(
                name: "CurrentQuestId",
                table: "Assignees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignees_CurrentQuestId",
                table: "Assignees",
                column: "CurrentQuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignees_Quests_CurrentQuestId",
                table: "Assignees",
                column: "CurrentQuestId",
                principalTable: "Quests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignees_Quests_CurrentQuestId",
                table: "Assignees");

            migrationBuilder.DropIndex(
                name: "IX_Assignees_CurrentQuestId",
                table: "Assignees");

            migrationBuilder.DropColumn(
                name: "CurrentQuestId",
                table: "Assignees");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "Quests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quests_AssigneeId",
                table: "Quests",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Assignees_AssigneeId",
                table: "Quests",
                column: "AssigneeId",
                principalTable: "Assignees",
                principalColumn: "Id");
        }
    }
}
