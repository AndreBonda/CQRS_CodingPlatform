using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addtiptable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<byte>(type: "smallint", nullable: false),
                    ChallengeDBId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tips_Challenges_ChallengeDBId",
                        column: x => x.ChallengeDBId,
                        principalTable: "Challenges",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tips_ChallengeDBId",
                table: "Tips",
                column: "ChallengeDBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tips");
        }
    }
}
