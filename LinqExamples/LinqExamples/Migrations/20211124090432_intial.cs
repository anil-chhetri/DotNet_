using Microsoft.EntityFrameworkCore.Migrations;

namespace LinqExamples.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "Decimal(18,2)", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "Location", "Name" },
                values: new object[] { 1, "New York", "IT" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "Location", "Name" },
                values: new object[] { 2, "London", "HR" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "Location", "Name" },
                values: new object[] { 3, "Sydney", "Payroll" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "DepartmentID", "FirstName", "Gender", "LastName", "Salary" },
                values: new object[,]
                {
                    { 1, 1, "Mark", "Male", "Hasting", 60000.0m },
                    { 3, 1, "Ben", "Male", "Hoskins", 70000.0m },
                    { 7, 1, "John", "Male", "Stanmore", 80000.0m },
                    { 4, 2, "Philp", "Male", "Hastings", 45000.0m },
                    { 5, 2, "Mary", "Female", "Lambeth", 30000.0m },
                    { 2, 3, "Steve", "Male", "Pound", 45000.0m },
                    { 6, 3, "Valarie", "Female", "Vikings", 35000.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                column: "DepartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
