using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CodeFirst.Migrations
{
    public partial class addColumnForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastLoginAddress",
                table: "User",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "PrevLoginAddress",
                table: "User",
                nullable: true);
            migrationBuilder.AddColumn<DateTime>(
                name: "PrevLoginTime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "LastLoginAddress", table: "User");
            migrationBuilder.DropColumn(name: "PrevLoginAddress", table: "User");
            migrationBuilder.DropColumn(name: "PrevLoginTime", table: "User");
        }
    }
}
