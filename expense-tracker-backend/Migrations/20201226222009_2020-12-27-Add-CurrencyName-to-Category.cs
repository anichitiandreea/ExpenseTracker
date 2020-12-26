using Microsoft.EntityFrameworkCore.Migrations;

namespace expense_tracker_backend.Migrations
{
    public partial class _20201227AddCurrencyNametoCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "Categories");
        }
    }
}
