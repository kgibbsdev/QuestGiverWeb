using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestGiver.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "Quests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Assignees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignees", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Assignees_AssigneeId",
                table: "Quests");

            migrationBuilder.DropTable(
                name: "Assignees");

            migrationBuilder.DropIndex(
                name: "IX_Quests_AssigneeId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Quests");
        }
    }
}
