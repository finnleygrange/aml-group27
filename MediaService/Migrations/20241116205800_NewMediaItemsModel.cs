using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaService.Migrations
{
    /// <inheritdoc />
    public partial class NewMediaItemsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "MediaItems");

            migrationBuilder.RenameColumn(
                name: "isAvailable",
                table: "MediaItems",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MediaItems",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "BorrowDurationDays",
                table: "MediaItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "MediaItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaItems",
                table: "MediaItems",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaItems",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "BorrowDurationDays",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "MediaItems");

            migrationBuilder.RenameTable(
                name: "MediaItems",
                newName: "Items");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Items",
                newName: "isAvailable");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Items",
                newName: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");
        }
    }
}
