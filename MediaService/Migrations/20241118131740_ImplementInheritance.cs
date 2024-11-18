using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaService.Migrations
{
    /// <inheritdoc />
    public partial class ImplementInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "AgeRating",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "MediaItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IssueNumber",
                table: "MediaItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaTypeDiscriminator",
                table: "MediaItems",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PageCount",
                table: "MediaItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "MediaItems",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ImageUrl", "MediaTypeDiscriminator", "MediaTypeId", "PageCount", "Title" },
                values: new object[] { 1, "John Doe", "An example book description.", "123-456-789", "https://example.com/images/book.jpg", "Book", 1, 300, "Example Book" });

            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "Description", "Director", "DurationMinutes", "ImageUrl", "MediaTypeDiscriminator", "MediaTypeId", "ReleaseYear", "Title" },
                values: new object[] { 2, "An example DVD description.", "Jane Smith", 120, "https://example.com/images/dvd.jpg", "DVD", 2, 0, "Example DVD" });

            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "AgeRating", "Description", "Developer", "Genre", "ImageUrl", "MediaTypeDiscriminator", "MediaTypeId", "Platform", "Title" },
                values: new object[] { 3, "E", "An example game description.", "GameDev Studios", "Survival", "https://example.com/images/game.jpg", "Game", 3, "PC", "Example Game" });

            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "Description", "ImageUrl", "IssueNumber", "MediaTypeDiscriminator", "MediaTypeId", "Publisher", "Title" },
                values: new object[] { 4, "An example journal description.", "https://example.com/images/journal.jpg", 42, "Journal", 4, "Science Journal Press", "Example Journal" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "AgeRating",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "Developer",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "IssueNumber",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "MediaTypeDiscriminator",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "PageCount",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "MediaItems");
        }
    }
}
