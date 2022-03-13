using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIPortal.Migrations
{
    public partial class NewMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageGroups",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageGroups", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "MessageConnections",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageGroupName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageConnections", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_MessageConnections_MessageGroups_MessageGroupName",
                        column: x => x.MessageGroupName,
                        principalTable: "MessageGroups",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageConnections_MessageGroupName",
                table: "MessageConnections",
                column: "MessageGroupName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageConnections");

            migrationBuilder.DropTable(
                name: "MessageGroups");
        }
    }
}
