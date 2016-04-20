using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CodeFirst.Migrations
{
    public partial class removeColumnForUser_UnreceivedCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "UnReceivedCount", table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnReceivedCount",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }
    }
}
