using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Core.Migrations
{
    public partial class IdentityUser_Distributor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Distributor",
                table: "AbpUsers",
                type: "uuid",
                nullable: true,
                // defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
                defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distributor",
                table: "AbpUsers");
        }
    }
}
