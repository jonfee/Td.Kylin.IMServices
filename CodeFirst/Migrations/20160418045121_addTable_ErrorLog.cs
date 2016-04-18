using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CodeFirst.Migrations
{
    public partial class addTable_ErrorLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLog",
                columns: table => new
                {
                    LogID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    HelpLink = table.Column<string>(type: "varchar(200)", nullable: true),
                    Message = table.Column<string>(type: "varchar(200)", nullable: true),
                    Source = table.Column<string>(type: "varchar(200)", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.LogID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ErrorLog");
        }
    }
}
