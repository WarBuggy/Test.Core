using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Core.Migrations
{
    public partial class Distributor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestMDMDistributors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    TaxId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestMDMDistributors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InquiryDistributorIdentityUser",
                columns: table => new
                {
                    DistributorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdentityUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryDistributorIdentityUser", x => new { x.DistributorId, x.IdentityUserId });
                    table.ForeignKey(
                        name: "FK_InquiryDistributorIdentityUser_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryDistributorIdentityUser_TestMDMDistributors_Distribu~",
                        column: x => x.DistributorId,
                        principalTable: "TestMDMDistributors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InquiryDistributorIdentityUser_DistributorId_IdentityUserId",
                table: "InquiryDistributorIdentityUser",
                columns: new[] { "DistributorId", "IdentityUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_InquiryDistributorIdentityUser_IdentityUserId",
                table: "InquiryDistributorIdentityUser",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InquiryDistributorIdentityUser");

            migrationBuilder.DropTable(
                name: "TestMDMDistributors");
        }
    }
}
