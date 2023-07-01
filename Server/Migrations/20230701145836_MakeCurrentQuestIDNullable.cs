using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestGiver.Server.Migrations
{
    /// <inheritdoc />
    public partial class MakeCurrentQuestIDNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignees_Quests_CurrentQuestId",
                table: "Assignees");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentQuestId",
                table: "Assignees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "CurrentQuestId",
                table: "Assignees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignees_Quests_CurrentQuestId",
                table: "Assignees",
                column: "CurrentQuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
