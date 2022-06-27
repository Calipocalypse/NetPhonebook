using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteDataProvider.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtraCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtraInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraInfos_ExtraCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExtraCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraInfos_CategoryId",
                table: "ExtraInfos",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraInfos");

            migrationBuilder.DropTable(
                name: "ExtraCategories");
        }
    }
}
