using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestGiver.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestLogId",
                table: "Quests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestLogId",
                table: "Assignees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quests_QuestLogId",
                table: "Quests",
                column: "QuestLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignees_QuestLogId",
                table: "Assignees",
                column: "QuestLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignees_QuestLogs_QuestLogId",
                table: "Assignees",
                column: "QuestLogId",
                principalTable: "QuestLogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_QuestLogs_QuestLogId",
                table: "Quests",
                column: "QuestLogId",
                principalTable: "QuestLogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignees_QuestLogs_QuestLogId",
                table: "Assignees");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_QuestLogs_QuestLogId",
                table: "Quests");

            migrationBuilder.DropTable(
                name: "QuestLogs");

            migrationBuilder.DropIndex(
                name: "IX_Quests_QuestLogId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Assignees_QuestLogId",
                table: "Assignees");

            migrationBuilder.DropColumn(
                name: "QuestLogId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "QuestLogId",
                table: "Assignees");
        }
    }
}
