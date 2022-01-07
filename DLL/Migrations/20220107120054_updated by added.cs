using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class updatedbyadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lastUpdatedBy",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastUpdatedBy",
                table: "Students");
        }
    }
}
