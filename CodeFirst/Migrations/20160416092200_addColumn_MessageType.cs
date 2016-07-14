using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst.Migrations
{
    public partial class addColumn_MessageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "UnreadMessage",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "MessageHistory",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "LastMessage",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "MessageType", table: "UnreadMessage");
            migrationBuilder.DropColumn(name: "MessageType", table: "MessageHistory");
            migrationBuilder.DropColumn(name: "MessageType", table: "LastMessage");
        }
    }
}
