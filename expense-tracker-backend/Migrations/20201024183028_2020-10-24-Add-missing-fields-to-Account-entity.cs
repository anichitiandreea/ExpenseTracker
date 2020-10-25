using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace expense_tracker_backend.Migrations
{
    public partial class _20201024AddmissingfieldstoAccountentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Currencies_CurrencyId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CurrencyId",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Accounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconColor",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IconColor",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CurrencyId",
                table: "Categories",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Currencies_CurrencyId",
                table: "Categories",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
