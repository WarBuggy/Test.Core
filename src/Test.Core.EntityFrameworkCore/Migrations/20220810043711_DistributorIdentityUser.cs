using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Core.Migrations
{
    public partial class DistributorIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TaxId",
                table: "TestMDMDistributors",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "TestMDMDistributors",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "TestMDMDistributorIdentityUser",
                columns: table => new
                {
                    DistributorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdentityUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestMDMDistributorIdentityUser", x => new { x.DistributorId, x.IdentityUserId });
                    table.ForeignKey(
                        name: "FK_TestMDMDistributorIdentityUser_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestMDMDistributorIdentityUser_TestMDMDistributors_Distribu~",
                        column: x => x.DistributorId,
                        principalTable: "TestMDMDistributors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestMDMDistributorIdentityUser_DistributorId_IdentityUserId",
                table: "TestMDMDistributorIdentityUser",
                columns: new[] { "DistributorId", "IdentityUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_TestMDMDistributorIdentityUser_IdentityUserId",
                table: "TestMDMDistributorIdentityUser",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestMDMDistributorIdentityUser");

            migrationBuilder.AlterColumn<string>(
                name: "TaxId",
                table: "TestMDMDistributors",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "TestMDMDistributors",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);
        }
    }
}
