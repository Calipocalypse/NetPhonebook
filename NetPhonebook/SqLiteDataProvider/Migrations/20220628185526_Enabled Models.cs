using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteDataProvider.Migrations
{
    public partial class EnabledModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "virtualModelsCustomizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CellId = table.Column<sbyte>(type: "INTEGER", nullable: false),
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
                        name: "FK_virtualModelsCustomizations_virtualModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "virtualModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_virtualModelsCustomizations_ModelId",
                table: "virtualModelsCustomizations",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "virtualModelsCustomizations");

            migrationBuilder.DropTable(
                name: "virtualModels");
        }
    }
}
