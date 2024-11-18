using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediaService.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedExampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "Description", "ImageUrl", "MediaType", "Title" },
                values: new object[,]
                {
                    { 1, "An example book description.", "https://example.com/images/book.jpg", "Book", "Example Book" },
                    { 2, "An example DVD description.", "https://example.com/images/dvd.jpg", "DVD", "Example DVD" },
                    { 3, "An example game description.", "https://example.com/images/game.jpg", "Game", "Example Game" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
