using Microsoft.EntityFrameworkCore.Migrations;

namespace AbrasNigEnt.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionGroups_Machines_MachineId",
                table: "SectionGroups");

            migrationBuilder.DropIndex(
                name: "IX_SectionGroups_MachineId",
                table: "SectionGroups");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "SectionGroups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "SectionGroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionGroups_MachineId",
                table: "SectionGroups",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionGroups_Machines_MachineId",
                table: "SectionGroups",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "MachineId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
