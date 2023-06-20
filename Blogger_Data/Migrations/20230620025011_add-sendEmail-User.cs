using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogger_Data.Migrations
{
    public partial class addsendEmailUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SendEmail",
                table: "Users",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendEmail",
                table: "Users");
        }
    }
}
