using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "Developer",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "DVDs");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "DVDs");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "MediaItems",
                newName: "Genre");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "MediaItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Genre" },
                values: new object[] { "John Doe", "Fiction" });

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "Genre" },
                values: new object[] { "Jane Doe", "Documentary" });

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "Genre" },
                values: new object[] { "Game Studio", "Action" });

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Author", "Genre" },
                values: new object[] { "Editor Team", "Science" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "MediaItems");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "MediaItems",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Journals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "DVDs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "DVDs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Author",
                value: "John Doe");

            migrationBuilder.UpdateData(
                table: "DVDs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Director", "ReleaseYear" },
                values: new object[] { "Jane Smith", 2020 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Developer", "Genre" },
                values: new object[] { "GameDev Studios", "Action" });

            migrationBuilder.UpdateData(
                table: "Journals",
                keyColumn: "Id",
                keyValue: 4,
                column: "Publisher",
                value: "Science Publishing");

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://covers.openlibrary.org/w/id/851077-M.jpg");

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://example.com/images/dvd.jpg");

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://example.com/images/game.jpg");

            migrationBuilder.UpdateData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://example.com/images/journal.jpg");
        }
    }
}
