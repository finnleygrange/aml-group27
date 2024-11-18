using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediaService.Migrations
{
    /// <inheritdoc />
    public partial class SeperateMediaTypeIntoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "MediaItems");

            migrationBuilder.AddColumn<int>(
                name: "MediaTypeId",
                table: "MediaItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MediaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "MediaTypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "MediaTypeId",
                value: 2);

            migrationBuilder.InsertData(
                table: "MediaTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Book" },
                    { 2, "DVD" },
                    { 3, "Game" },
                    { 4, "Journal" },
                    { 5, "Multimedia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_MediaTypeId",
                table: "MediaItems",
                column: "MediaTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItems_MediaTypes_MediaTypeId",
                table: "MediaItems",
                column: "MediaTypeId",
                principalTable: "MediaTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItems_MediaTypes_MediaTypeId",
                table: "MediaItems");

            migrationBuilder.DropTable(
                name: "MediaTypes");

            migrationBuilder.DropIndex(
                name: "IX_MediaItems_MediaTypeId",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "MediaTypeId",
                table: "MediaItems");

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "MediaType",
                value: "Book");

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "MediaType",
                value: "DVD");

            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "Description", "ImageUrl", "MediaType", "Title" },
                values: new object[] { 3, "An example game description.", "https://example.com/images/game.jpg", "Game", "Example Game" });
        }
    }
}
