using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaService.Migrations
{
    /// <inheritdoc />
    public partial class SeperateMediaTypesByTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_MediaItems_Id",
                        column: x => x.Id,
                        principalTable: "MediaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DVDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DVDs_MediaItems_Id",
                        column: x => x.Id,
                        principalTable: "MediaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_MediaItems_Id",
                        column: x => x.Id,
                        principalTable: "MediaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journals_MediaItems_Id",
                        column: x => x.Id,
                        principalTable: "MediaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "ISBN", "PageCount" },
                values: new object[] { 1, "John Doe", "123-456-789", 300 });

            migrationBuilder.InsertData(
                table: "DVDs",
                columns: new[] { "Id", "Director", "DurationMinutes", "ReleaseYear" },
                values: new object[] { 2, "Jane Smith", 120, 2020 });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AgeRating", "Developer", "Genre", "Platform" },
                values: new object[] { 3, "Mature", "GameDev Studios", "Action", "PC" });

            migrationBuilder.InsertData(
                table: "Journals",
                columns: new[] { "Id", "IssueNumber", "Publisher" },
                values: new object[] { 4, 12, "Science Publishing" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "DVDs");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Journals");

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

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: 4);

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
    }
}
