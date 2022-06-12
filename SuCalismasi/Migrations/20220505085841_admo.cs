using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Water.Migrations
{
    public partial class admo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Branches",
                type: "nchar(50)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Branches");
        }
    }
}
