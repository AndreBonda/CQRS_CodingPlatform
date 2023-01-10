using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changeorderfrombytetoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Tips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Challenges",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Challenges");

            migrationBuilder.AlterColumn<byte>(
                name: "Order",
                table: "Tips",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
