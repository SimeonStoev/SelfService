using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfServiceHrProject.Migrations
{
    public partial class ModifyDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Address_AddressId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Salary_SalaryId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AddressId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SalaryId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SalaryId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeesId",
                table: "Salary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeesId",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmployeesId",
                table: "Salary",
                column: "EmployeesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_EmployeesId",
                table: "Address",
                column: "EmployeesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Employees_EmployeesId",
                table: "Address",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salary_Employees_EmployeesId",
                table: "Salary",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Employees_EmployeesId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Salary_Employees_EmployeesId",
                table: "Salary");

            migrationBuilder.DropIndex(
                name: "IX_Salary_EmployeesId",
                table: "Salary");

            migrationBuilder.DropIndex(
                name: "IX_Address_EmployeesId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "EmployeesId",
                table: "Salary");

            migrationBuilder.DropColumn(
                name: "EmployeesId",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalaryId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AddressId",
                table: "Employees",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SalaryId",
                table: "Employees",
                column: "SalaryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Address_AddressId",
                table: "Employees",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Salary_SalaryId",
                table: "Employees",
                column: "SalaryId",
                principalTable: "Salary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
