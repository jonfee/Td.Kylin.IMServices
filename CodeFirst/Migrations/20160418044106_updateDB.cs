using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CodeFirst.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "UnreadCount", table: "User");
            migrationBuilder.DropColumn(name: "IsRead", table: "MessageHistory");
            migrationBuilder.DropColumn(name: "IsRead", table: "LastMessage");
            migrationBuilder.DropTable("UnreadMessage");
            migrationBuilder.CreateTable(
                name: "UnSendMessage",
                columns: table => new
                {
                    MessageID = table.Column<long>(nullable: false),
                    Content = table.Column<string>(type: "varchar(500)", nullable: true),
                    MessageType = table.Column<int>(nullable: false),
                    Receiver = table.Column<long>(nullable: false),
                    SendTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnSendMessage", x => x.MessageID);
                });
            migrationBuilder.AddColumn<int>(
                name: "UnReceivedCount",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "UnReceivedCount", table: "User");
            migrationBuilder.DropTable("UnSendMessage");
            migrationBuilder.CreateTable(
                name: "UnreadMessage",
                columns: table => new
                {
                    MessageID = table.Column<long>(nullable: false),
                    Content = table.Column<string>(type: "varchar(500)", nullable: true),
                    MessageType = table.Column<int>(nullable: false),
                    Receiver = table.Column<long>(nullable: false),
                    SendTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnreadMessage", x => x.MessageID);
                });
            migrationBuilder.AddColumn<int>(
                name: "UnreadCount",
                table: "User",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "MessageHistory",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "LastMessage",
                nullable: false,
                defaultValue: false);
        }
    }
}
