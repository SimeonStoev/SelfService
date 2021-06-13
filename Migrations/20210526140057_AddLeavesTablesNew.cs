using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfServiceHrProject.Migrations
{
    public partial class AddLeavesTablesNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveReasonsId",
                table: "Leaves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_LeaveReasonsId",
                table: "Leaves",
                column: "LeaveReasonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_LeaveReasons_LeaveReasonsId",
                table: "Leaves",
                column: "LeaveReasonsId",
                principalTable: "LeaveReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_LeaveReasons_LeaveReasonsId",
                table: "Leaves");

            migrationBuilder.DropIndex(
                name: "IX_Leaves_LeaveReasonsId",
                table: "Leaves");

            migrationBuilder.DropColumn(
                name: "LeaveReasonsId",
                table: "Leaves");
        }
    }
}
