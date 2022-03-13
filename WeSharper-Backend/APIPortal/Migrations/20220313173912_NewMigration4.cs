using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    public partial class NewMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "SenderUsername",
                table: "Message",
                type: "nvarchar(450)",
                nullable: false
            );

            migrationBuilder.AddColumn<string>(
                name: "RecipientUsername",
                table: "Message",
                type: "nvarchar(450)",
                nullable: false
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderUsername",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "RecipientUsername",
                table: "Message");
        }
    }
}
