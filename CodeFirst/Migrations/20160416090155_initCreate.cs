using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CodeFirst.Migrations
{
    public partial class initCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LastMessage",
                columns: table => new
                {
                    MessageID = table.Column<long>(nullable: false),
                    Content = table.Column<string>(type: "varchar(500)", nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    ReadTime = table.Column<DateTime>(nullable: true),
                    Receiver = table.Column<long>(nullable: false),
                    ReceiverName = table.Column<string>(type: "varchar(50)", nullable: true),
                    SendTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastMessage", x => x.MessageID);
                });
            migrationBuilder.CreateTable(
                name: "MessageHistory",
                columns: table => new
                {
                    MessageID = table.Column<long>(nullable: false),
                    Content = table.Column<string>(type: "varchar(500)", nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    ReadTime = table.Column<DateTime>(nullable: true),
                    Receiver = table.Column<long>(nullable: false),
                    ReceiverName = table.Column<string>(type: "varchar(50)", nullable: true),
                    SendTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageHistory", x => x.MessageID);
                });
            migrationBuilder.CreateTable(
                name: "UnreadMessage",
                columns: table => new
                {
                    MessageID = table.Column<long>(nullable: false),
                    Content = table.Column<string>(type: "varchar(500)", nullable: true),
                    Receiver = table.Column<long>(nullable: false),
                    SendTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnreadMessage", x => x.MessageID);
                });
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    NickName = table.Column<string>(type: "varchar(50)", nullable: true),
                    Photo = table.Column<string>(type: "varchar(100)", nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UnreadCount = table.Column<int>(nullable: false),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("LastMessage");
            migrationBuilder.DropTable("MessageHistory");
            migrationBuilder.DropTable("UnreadMessage");
            migrationBuilder.DropTable("User");
        }
    }
}
