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
                name: "FK_virtualModelsCellDatas_ExtraInfos_PrefixId",
                table: "virtualModelsCellDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_virtualModelsCellDatas_ExtraInfos_SuffixId",
                table: "virtualModelsCellDatas");

            migrationBuilder.DropIndex(
                name: "IX_virtualModelsCellDatas_PrefixId",
                table: "virtualModelsCellDatas");

            migrationBuilder.DropIndex(
                name: "IX_virtualModelsCellDatas_SuffixId",
                table: "virtualModelsCellDatas");

            migrationBuilder.DropColumn(
                name: "IsUsingPrefix",
                table: "virtualModelsCellDatas");

            migrationBuilder.DropColumn(
                name: "IsUsingSuffix",
                table: "virtualModelsCellDatas");

            migrationBuilder.DropColumn(
                name: "PrefixId",
                table: "virtualModelsCellDatas");

            migrationBuilder.DropColumn(
                name: "SuffixId",
                table: "virtualModelsCellDatas");

            migrationBuilder.AddColumn<Guid>(
                name: "extraInfoId",
                table: "virtualModelsCellDatas",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_virtualModelsCellDatas_extraInfoId",
                table: "virtualModelsCellDatas",
                column: "extraInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_virtualModelsCellDatas_ExtraInfos_extraInfoId",
                table: "virtualModelsCellDatas",
                column: "extraInfoId",
                principalTable: "ExtraInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_virtualModelsCellDatas_ExtraInfos_extraInfoId",
                table: "virtualModelsCellDatas");

            migrationBuilder.DropIndex(
                name: "IX_virtualModelsCellDatas_extraInfoId",
                table: "virtualModelsCellDatas");

            migrationBuilder.DropColumn(
                name: "extraInfoId",
                table: "virtualModelsCellDatas");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsingPrefix",
                table: "virtualModelsCellDatas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsingSuffix",
                table: "virtualModelsCellDatas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PrefixId",
                table: "virtualModelsCellDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SuffixId",
                table: "virtualModelsCellDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_virtualModelsCellDatas_PrefixId",
                table: "virtualModelsCellDatas",
                column: "PrefixId");

            migrationBuilder.CreateIndex(
                name: "IX_virtualModelsCellDatas_SuffixId",
                table: "virtualModelsCellDatas",
                column: "SuffixId");

            migrationBuilder.AddForeignKey(
                name: "FK_virtualModelsCellDatas_ExtraInfos_PrefixId",
                table: "virtualModelsCellDatas",
                column: "PrefixId",
                principalTable: "ExtraInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_virtualModelsCellDatas_ExtraInfos_SuffixId",
                table: "virtualModelsCellDatas",
                column: "SuffixId",
                principalTable: "ExtraInfos",
                principalColumn: "Id");
        }
    }
}
