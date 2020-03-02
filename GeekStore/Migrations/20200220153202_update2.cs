using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStore.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 75);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ProductCarts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "ProductCarts");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserProfiles",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserProfiles",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
