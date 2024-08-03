using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestGiver.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToQuests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Quests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Quests");
        }
    }
}
