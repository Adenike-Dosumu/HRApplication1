using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRApplication1.Migrations
{
    public partial class addauditentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentType",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Departments",
                newName: "AuditEntity_Status");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Departments",
                newName: "AuditEntity_Name");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Departments",
                newName: "AuditEntity_ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "Departments",
                newName: "AuditEntity_DateModified");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Departments",
                newName: "AuditEntity_DateCreated");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Departments",
                newName: "AuditEntity_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "DepartmentTypeDesc",
                table: "Departments",
                newName: "DepartmentName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuditEntity_Status",
                table: "Departments",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "AuditEntity_Name",
                table: "Departments",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AuditEntity_ModifiedBy",
                table: "Departments",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "AuditEntity_DateModified",
                table: "Departments",
                newName: "DateModified");

            migrationBuilder.RenameColumn(
                name: "AuditEntity_DateCreated",
                table: "Departments",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "AuditEntity_CreatedBy",
                table: "Departments",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "DepartmentName",
                table: "Departments",
                newName: "DepartmentTypeDesc");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentType",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_DepartmentId",
                table: "Staffs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");
        }
    }
}
