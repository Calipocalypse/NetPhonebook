using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqLiteWithLinqDataProvider.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_virtualModelsCellDatas_ExtraInfos_extraInfoId",
                table: "virtualModelsCellDatas");

            migrationBuilder.AlterColumn<Guid>(
                name: "extraInfoId",
                table: "virtualModelsCellDatas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_virtualModelsCellDatas_ExtraInfos_extraInfoId",
                table: "virtualModelsCellDatas",
                column: "extraInfoId",
                principalTable: "ExtraInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_virtualModelsCellDatas_ExtraInfos_extraInfoId",
                table: "virtualModelsCellDatas");

            migrationBuilder.AlterColumn<Guid>(
                name: "extraInfoId",
                table: "virtualModelsCellDatas",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_virtualModelsCellDatas_ExtraInfos_extraInfoId",
                table: "virtualModelsCellDatas",
                column: "extraInfoId",
                principalTable: "ExtraInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
