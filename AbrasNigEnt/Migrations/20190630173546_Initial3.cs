using Microsoft.EntityFrameworkCore.Migrations;

namespace AbrasNigEnt.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "MachineSectionGroup",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MachineSectionGroup_SectionId",
                table: "MachineSectionGroup",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSectionGroup_Sections_SectionId",
                table: "MachineSectionGroup",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineSectionGroup_Sections_SectionId",
                table: "MachineSectionGroup");

            migrationBuilder.DropIndex(
                name: "IX_MachineSectionGroup_SectionId",
                table: "MachineSectionGroup");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "MachineSectionGroup");
        }
    }
}
