using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketplaceServices.Migrations
{
    public partial class adduseridservics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_userId",
                table: "Services",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_userId",
                table: "Services",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_userId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_userId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Services");
        }
    }
}
