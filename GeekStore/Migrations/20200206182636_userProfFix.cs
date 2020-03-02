using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStore.Migrations
{
    public partial class userProfFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostDepartament",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sity",
                table: "UserProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostDepartament",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Sity",
                table: "UserProfiles");
        }
    }
}
