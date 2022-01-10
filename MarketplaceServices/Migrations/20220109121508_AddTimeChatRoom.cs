using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketplaceServices.Migrations
{
    public partial class AddTimeChatRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "chatrooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "chatrooms");

           
        }
    }
}
