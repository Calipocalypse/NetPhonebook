using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteWithLinqDataProvider.Migrations
{
    public partial class NOTTHISMODEL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "virtualModels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorderColor",
                table: "virtualModels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorderSize",
                table: "virtualModels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CornerRadius",
                table: "virtualModels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontSize",
                table: "virtualModels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForegroundColor",
                table: "virtualModels",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "virtualModels");

            migrationBuilder.DropColumn(
                name: "BorderColor",
                table: "virtualModels");

            migrationBuilder.DropColumn(
                name: "BorderSize",
                table: "virtualModels");

            migrationBuilder.DropColumn(
                name: "CornerRadius",
                table: "virtualModels");

            migrationBuilder.DropColumn(
                name: "FontSize",
                table: "virtualModels");

            migrationBuilder.DropColumn(
                name: "ForegroundColor",
                table: "virtualModels");

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
    }
}
