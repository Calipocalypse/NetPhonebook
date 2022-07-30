using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteWithLinqDataProvider.Migrations
{
    public partial class AddedEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "CellType",
                table: "virtualModelsCustomizations",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellType",
                table: "virtualModelsCustomizations");
        }
    }
}
