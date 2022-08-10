using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteWithLinqDataProvider.Migrations
{
    public partial class init : Migration
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
                name: "favouriteColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    HexColor = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favouriteColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "virtualModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_virtualModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtraInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExtraCategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraInfos_ExtraCategories_ExtraCategoryId",
                        column: x => x.ExtraCategoryId,
                        principalTable: "ExtraCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "virtualModelsCustomizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CellId = table.Column<byte>(type: "INTEGER", nullable: false),
                    CellType = table.Column<byte>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BackgroundColor = table.Column<string>(type: "TEXT", nullable: true),
                    ForegroundColor = table.Column<string>(type: "TEXT", nullable: true),
                    BorderColor = table.Column<string>(type: "TEXT", nullable: true),
                    BorderSize = table.Column<string>(type: "TEXT", nullable: true),
                    CornerRadius = table.Column<string>(type: "TEXT", nullable: true),
                    FontSize = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_virtualModelsCustomizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_virtualModelsCustomizations_ExtraCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExtraCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_virtualModelsCustomizations_virtualModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "virtualModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "virtualModelsDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DisplayedNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ModelBaseId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_virtualModelsDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_virtualModelsDatas_virtualModels_ModelBaseId",
                        column: x => x.ModelBaseId,
                        principalTable: "virtualModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "virtualModelsCellDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MainDataId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CellId = table.Column<sbyte>(type: "INTEGER", nullable: false),
                    CellType = table.Column<byte>(type: "INTEGER", nullable: false),
                    FirstText = table.Column<string>(type: "TEXT", nullable: true),
                    SecondText = table.Column<string>(type: "TEXT", nullable: true),
                    extraInfoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_virtualModelsCellDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_virtualModelsCellDatas_ExtraInfos_extraInfoId",
                        column: x => x.extraInfoId,
                        principalTable: "ExtraInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_virtualModelsCellDatas_virtualModelsDatas_MainDataId",
                        column: x => x.MainDataId,
                        principalTable: "virtualModelsDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraInfos_ExtraCategoryId",
                table: "ExtraInfos",
                column: "ExtraCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_virtualModelsCellDatas_extraInfoId",
                table: "virtualModelsCellDatas",
                column: "extraInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_virtualModelsCellDatas_MainDataId",
                table: "virtualModelsCellDatas",
                column: "MainDataId");

            migrationBuilder.CreateIndex(
                name: "IX_virtualModelsCustomizations_CategoryId",
                table: "virtualModelsCustomizations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_virtualModelsCustomizations_ModelId",
                table: "virtualModelsCustomizations",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_virtualModelsDatas_ModelBaseId",
                table: "virtualModelsDatas",
                column: "ModelBaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favouriteColors");

            migrationBuilder.DropTable(
                name: "virtualModelsCellDatas");

            migrationBuilder.DropTable(
                name: "virtualModelsCustomizations");

            migrationBuilder.DropTable(
                name: "ExtraInfos");

            migrationBuilder.DropTable(
                name: "virtualModelsDatas");

            migrationBuilder.DropTable(
                name: "ExtraCategories");

            migrationBuilder.DropTable(
                name: "virtualModels");
        }
    }
}
