using Microsoft.EntityFrameworkCore.Migrations;

namespace DatascopeTest.Migrations
{
    public partial class MakingRatingRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Rating",
                table: "Games",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Rating",
                table: "Games",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte));
        }
    }
}
