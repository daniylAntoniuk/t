using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStore.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PostDepartament",
                table: "UserProfiles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PostDepartament",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
