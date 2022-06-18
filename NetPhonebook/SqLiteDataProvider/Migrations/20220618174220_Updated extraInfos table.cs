using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteDataProvider.Migrations
{
    public partial class UpdatedextraInfostable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraInfos_ExtraCategories_CategoryId",
                table: "ExtraInfos");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExtraInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "ExtraInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraInfos_ExtraCategories_CategoryId",
                table: "ExtraInfos",
                column: "CategoryId",
                principalTable: "ExtraCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraInfos_ExtraCategories_CategoryId",
                table: "ExtraInfos");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExtraInfos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "ExtraInfos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraInfos_ExtraCategories_CategoryId",
                table: "ExtraInfos",
                column: "CategoryId",
                principalTable: "ExtraCategories",
                principalColumn: "Id");
        }
    }
}
