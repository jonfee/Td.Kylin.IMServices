using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CodeFirst.Migrations
{
    public partial class addColumnForErrorLog_Tag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLoginRecords",
                columns: table => new
                {
                    RecordID = table.Column<long>(nullable: false),
                    AreaID = table.Column<int>(nullable: false),
                    AreaName = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    LoginTime = table.Column<DateTime>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    TerminalDevice = table.Column<int>(nullable: false),
                    UserID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginRecords", x => x.RecordID);
                });
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "ErrorLog",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "LastLoginTime", table: "User");
            migrationBuilder.DropColumn(name: "Tag", table: "ErrorLog");
            migrationBuilder.DropTable("UserLoginRecords");
        }
    }
}
