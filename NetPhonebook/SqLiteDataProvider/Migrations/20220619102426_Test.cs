using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteDataProvider.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraInfos_ExtraCategories_CategoryId",
                table: "ExtraInfos");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ExtraInfos",
                newName: "ExtraCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraInfos_CategoryId",
                table: "ExtraInfos",
                newName: "IX_ExtraInfos_ExtraCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraInfos_ExtraCategories_ExtraCategoryId",
                table: "ExtraInfos",
                column: "ExtraCategoryId",
                principalTable: "ExtraCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraInfos_ExtraCategories_ExtraCategoryId",
                table: "ExtraInfos");

            migrationBuilder.RenameColumn(
                name: "ExtraCategoryId",
                table: "ExtraInfos",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraInfos_ExtraCategoryId",
                table: "ExtraInfos",
                newName: "IX_ExtraInfos_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraInfos_ExtraCategories_CategoryId",
                table: "ExtraInfos",
                column: "CategoryId",
                principalTable: "ExtraCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
