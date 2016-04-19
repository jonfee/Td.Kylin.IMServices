using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CodeFirst.Migrations
{
    public partial class addColumnForUnSendMessage_SenderName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Receiver", table: "UnSendMessage");
            migrationBuilder.DropColumn(name: "UserID", table: "UnSendMessage");
            migrationBuilder.DropColumn(name: "UserName", table: "UnSendMessage");
            migrationBuilder.DropColumn(name: "Receiver", table: "MessageHistory");
            migrationBuilder.DropColumn(name: "UserID", table: "MessageHistory");
            migrationBuilder.DropColumn(name: "UserName", table: "MessageHistory");
            migrationBuilder.DropTable("LastMessage");
            migrationBuilder.CreateTable(
                name: "UserRelation",
                columns: table => new
                {
                    FirstUser = table.Column<long>(nullable: false),
                    SecondUser = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    FirstDelete = table.Column<bool>(nullable: false),
                    FirstGroupName = table.Column<string>(nullable: true),
                    SecondDelete = table.Column<bool>(nullable: false),
                    SecondGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRelation", x => new { x.FirstUser, x.SecondUser });
                });
            migrationBuilder.AddColumn<long>(
                name: "ReceiverID",
                table: "UnSendMessage",
                nullable: false,
                defaultValue: 0L);
            migrationBuilder.AddColumn<long>(
                name: "SenderID",
                table: "UnSendMessage",
                nullable: false,
                defaultValue: 0L);
            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "UnSendMessage",
                type: "varchar(50)",
                nullable: true);
            migrationBuilder.AddColumn<long>(
                name: "ReceiverID",
                table: "MessageHistory",
                nullable: false,
                defaultValue: 0L);
            migrationBuilder.AddColumn<long>(
                name: "SenderID",
                table: "MessageHistory",
                nullable: false,
                defaultValue: 0L);
            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "MessageHistory",
                type: "varchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "ReceiverID", table: "UnSendMessage");
            migrationBuilder.DropColumn(name: "SenderID", table: "UnSendMessage");
            migrationBuilder.DropColumn(name: "SenderName", table: "UnSendMessage");
            migrationBuilder.DropColumn(name: "ReceiverID", table: "MessageHistory");
            migrationBuilder.DropColumn(name: "SenderID", table: "MessageHistory");
            migrationBuilder.DropColumn(name: "SenderName", table: "MessageHistory");
            migrationBuilder.DropTable("UserRelation");
            migrationBuilder.CreateTable(
                name: "LastMessage",
                columns: table => new
                {
                    MessageID = table.Column<long>(nullable: false),
                    Content = table.Column<string>(type: "varchar(500)", nullable: true),
                    MessageType = table.Column<int>(nullable: false),
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
            migrationBuilder.AddColumn<long>(
                name: "Receiver",
                table: "UnSendMessage",
                nullable: false,
                defaultValue: 0L);
            migrationBuilder.AddColumn<long>(
                name: "UserID",
                table: "UnSendMessage",
                nullable: false,
                defaultValue: 0L);
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UnSendMessage",
                type: "varchar(50)",
                nullable: true);
            migrationBuilder.AddColumn<long>(
                name: "Receiver",
                table: "MessageHistory",
                nullable: false,
                defaultValue: 0L);
            migrationBuilder.AddColumn<long>(
                name: "UserID",
                table: "MessageHistory",
                nullable: false,
                defaultValue: 0L);
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "MessageHistory",
                type: "varchar(50)",
                nullable: true);
        }
    }
}
