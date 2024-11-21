using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://covers.openlibrary.org/w/id/851077-M.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://example.com/images/book.jpg");
        }
    }
}
