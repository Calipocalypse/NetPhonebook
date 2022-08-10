using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteWithLinqDataProvider.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_virtualModelsCustomizations_ExtraCategories_CategoryId",
                table: "virtualModelsCustomizations");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "virtualModelsCustomizations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_virtualModelsCustomizations_ExtraCategories_CategoryId",
                table: "virtualModelsCustomizations",
                column: "CategoryId",
                principalTable: "ExtraCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_virtualModelsCustomizations_ExtraCategories_CategoryId",
                table: "virtualModelsCustomizations");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "virtualModelsCustomizations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_virtualModelsCustomizations_ExtraCategories_CategoryId",
                table: "virtualModelsCustomizations",
                column: "CategoryId",
                principalTable: "ExtraCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
