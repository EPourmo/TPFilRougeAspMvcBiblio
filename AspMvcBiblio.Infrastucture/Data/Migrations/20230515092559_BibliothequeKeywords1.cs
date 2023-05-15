using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMvcBiblio.Data.Migrations
{
    public partial class BibliothequeKeywords1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyWords_Books_BookId",
                table: "KeyWords");

            migrationBuilder.DropIndex(
                name: "IX_KeyWords_BookId",
                table: "KeyWords");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "KeyWords");

            migrationBuilder.CreateTable(
                name: "BookKeyword",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    KeyWordsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookKeyword", x => new { x.BooksId, x.KeyWordsId });
                    table.ForeignKey(
                        name: "FK_BookKeyword_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookKeyword_KeyWords_KeyWordsId",
                        column: x => x.KeyWordsId,
                        principalTable: "KeyWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KeyWords",
                columns: new[] { "Id", "Word" },
                values: new object[] { 2, "meurtre" });

            migrationBuilder.InsertData(
                table: "KeyWords",
                columns: new[] { "Id", "Word" },
                values: new object[] { 3, "été" });

            migrationBuilder.InsertData(
                table: "KeyWords",
                columns: new[] { "Id", "Word" },
                values: new object[] { 4, "voyage" });

            migrationBuilder.InsertData(
                table: "BookKeyword",
                columns: new[] { "BooksId", "KeyWordsId" },
                values: new object[] { 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_BookKeyword_KeyWordsId",
                table: "BookKeyword",
                column: "KeyWordsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookKeyword");

            migrationBuilder.DeleteData(
                table: "KeyWords",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "KeyWords",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "KeyWords",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "KeyWords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_BookId",
                table: "KeyWords",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyWords_Books_BookId",
                table: "KeyWords",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
