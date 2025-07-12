using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdoptionMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPetAdModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "PetAds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "PetAds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PetAds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "PetAds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PetAds_UserId",
                table: "PetAds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetAds_AspNetUsers_UserId",
                table: "PetAds",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetAds_AspNetUsers_UserId",
                table: "PetAds");

            migrationBuilder.DropIndex(
                name: "IX_PetAds_UserId",
                table: "PetAds");

            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "PetAds");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PetAds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PetAds");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "PetAds");
        }
    }
}
