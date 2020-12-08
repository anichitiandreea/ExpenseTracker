using Microsoft.EntityFrameworkCore.Migrations;

namespace expense_tracker_backend.Migrations
{
    public partial class _20201208AddCurrencyNametoAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "Accounts");
        }
    }
}
