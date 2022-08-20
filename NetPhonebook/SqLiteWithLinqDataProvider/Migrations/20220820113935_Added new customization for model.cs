using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteWithLinqDataProvider.Migrations
{
    public partial class Addednewcustomizationformodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "virtualModelsDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorderColor",
                table: "virtualModelsDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorderSize",
                table: "virtualModelsDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CornerRadius",
                table: "virtualModelsDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontSize",
                table: "virtualModelsDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForegroundColor",
                table: "virtualModelsDatas",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "virtualModelsDatas");

            migrationBuilder.DropColumn(
                name: "BorderColor",
                table: "virtualModelsDatas");

            migrationBuilder.DropColumn(
                name: "BorderSize",
                table: "virtualModelsDatas");

            migrationBuilder.DropColumn(
                name: "CornerRadius",
                table: "virtualModelsDatas");

            migrationBuilder.DropColumn(
                name: "FontSize",
                table: "virtualModelsDatas");

            migrationBuilder.DropColumn(
                name: "ForegroundColor",
                table: "virtualModelsDatas");
        }
    }
}
