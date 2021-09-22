using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItemzApp.API.Migrations
{
    public partial class Introducing_entity_ItemzJoinItemzTrace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemzJoinItemzTrace",
                columns: table => new
                {
                    FromItemzId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToItemzId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemzJoinItemzTrace", x => new { x.FromItemzId, x.ToItemzId });
                    table.ForeignKey(
                        name: "FK_ItemzJoinItemzTrace_Itemzs_FromItemzId",
                        column: x => x.FromItemzId,
                        principalTable: "Itemzs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemzJoinItemzTrace_Itemzs_ToItemzId",
                        column: x => x.ToItemzId,
                        principalTable: "Itemzs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemzJoinItemzTrace_ToItemzId",
                table: "ItemzJoinItemzTrace",
                column: "ToItemzId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemzJoinItemzTrace");
        }
    }
}
