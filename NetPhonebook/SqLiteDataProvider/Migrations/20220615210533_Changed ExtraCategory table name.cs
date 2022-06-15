using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteDataProvider.Migrations
{
    public partial class ChangedExtraCategorytablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraCategory",
                table: "ExtraCategory");

            migrationBuilder.RenameTable(
                name: "ExtraCategory",
                newName: "ExtraCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraCategories",
                table: "ExtraCategories",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraCategories",
                table: "ExtraCategories");

            migrationBuilder.RenameTable(
                name: "ExtraCategories",
                newName: "ExtraCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraCategory",
                table: "ExtraCategory",
                column: "Id");
        }
    }
}
