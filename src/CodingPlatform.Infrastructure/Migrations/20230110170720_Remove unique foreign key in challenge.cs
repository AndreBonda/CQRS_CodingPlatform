using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Removeuniqueforeignkeyinchallenge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Challenges_AdminId",
                table: "Challenges");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_AdminId",
                table: "Challenges",
                column: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Challenges_AdminId",
                table: "Challenges");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_AdminId",
                table: "Challenges",
                column: "AdminId",
                unique: true);
        }
    }
}
