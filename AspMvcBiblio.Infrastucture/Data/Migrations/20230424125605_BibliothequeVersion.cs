using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMvcBiblio.Data.Migrations
{
    public partial class BibliothequeVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_BookId",
                table: "Entity");

            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Entity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Keyword_BookId",
                table: "Entity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entity_AuthorId",
                table: "Entity",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_Keyword_BookId",
                table: "Entity",
                column: "Keyword_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_AuthorId",
                table: "Entity",
                column: "AuthorId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_BookId",
                table: "Entity",
                column: "BookId",
                principalTable: "Entity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_Keyword_BookId",
                table: "Entity",
                column: "Keyword_BookId",
                principalTable: "Entity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_AuthorId",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_BookId",
                table: "Entity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_Keyword_BookId",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_AuthorId",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_Keyword_BookId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "Keyword_BookId",
                table: "Entity");

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Entity_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Entity_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_BookId",
                table: "Entity",
                column: "BookId",
                principalTable: "Entity",
                principalColumn: "Id");
        }
    }
}
