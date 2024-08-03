using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestGiver.Server.Migrations
{
	/// <inheritdoc />
	public partial class AddAssignedQuestLogIdToQuests : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "AssignedQuestLogId",
				table: "Quests",
				type: "int",
				nullable: true,
				defaultValue: null);

			migrationBuilder.AddForeignKey(
				name: "FK_Quests_QuestLogs_AssignedQuestLogId",
				table: "Quests",
				column: "AssignedQuestLogId",
				principalTable: "QuestLogs",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "AssignedQuestLogId",
				table: "Quests");

			migrationBuilder.DropForeignKey(
				name: "FK_Quests_QuestLogs_AssignedQuestLogId",
				table: "Quests");
		}
	}
}
