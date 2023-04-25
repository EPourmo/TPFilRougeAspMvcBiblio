using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMvcBiblio.Data.Migrations
{
    public partial class BibliothequeVersionLilian1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CopiesNumber = table.Column<int>(type: "int", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomainName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

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
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyWords_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookTheme",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    ThemesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTheme", x => new { x.BooksId, x.ThemesId });
                    table.ForeignKey(
                        name: "FK_BookTheme_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTheme_Themes_ThemesId",
                        column: x => x.ThemesId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 1, "Jane", "Austen" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CopiesNumber", "ISBN", "ServiceDate", "Title" },
                values: new object[] { 1, 1, "2-7654-1005-4", new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Orgueil et Préjugés" });

            migrationBuilder.InsertData(
                table: "KeyWords",
                columns: new[] { "Id", "BookId", "Word" },
                values: new object[] { 1, null, "mariage" });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "Description", "DomainName" },
                values: new object[,]
                {
                    { 1, "Les livres de suspense sont des thrillers qui tiennent les lecteurs en haleine jusqu'à la fin. Ils ont souvent des intrigues complexes, des personnages intrigants et des rebondissements inattendus. \"Gone Girl\" de Gillian Flynn est un exemple de livre de suspense qui raconte l'histoire d'un mari qui devient le suspect principal dans la disparition de sa femme", "Suspense " },
                    { 2, "Les biographies sont des livres qui racontent la vie d'une personne réelle. Ils peuvent se concentrer sur la vie entière de la personne ou sur une période spécifique de sa vie. Les biographies sont souvent utilisées pour en apprendre davantage sur des personnalités célèbres telles que des musiciens, des acteurs ou des politiciens. \"Steve Jobs\" de Walter Isaacson est un exemple de biographie bien connue.", "Biographie " },
                    { 3, "Les livres de fantasy sont des histoires qui se déroulent dans des mondes imaginaires remplis de magie et de créatures fantastiques. Ils peuvent se concentrer sur des aventures, des quêtes ou des batailles épiques. \"Le Seigneur des Anneaux\" de J.R.R. Tolkien est un exemple bien connu de livre de fantasy.", "Fantasy" },
                    { 4, "Les livres de romance sont des histoires d'amour passionnantes et souvent dramatiques. Ils peuvent se concentrer sur des relations amoureuses, des triangles amoureux et des obstacles à surmonter. \"Orgueil et Préjugés\" de Jane Austen est un exemple classique de livre de romance.", "Romance " },
                    { 5, "Les livres policiers sont des histoires qui se concentrent sur les enquêtes et les résolutions de crimes. Les auteurs de livres policiers doivent créer des intrigues compliquées et des personnages convaincants pour garder les lecteurs captivés. \"Le Silence des Agneaux\" de Thomas Harris est un exemple célèbre de livre policier.", "Policier " },
                    { 6, "Les autobiographies sont des livres qui racontent la vie d'une personne écrite par elle-même. Ils peuvent se concentrer sur des moments clés de la vie de l'auteur, des leçons apprises ou des défis surmontés. \"Born a Crime\" de Trevor Noah est un exemple récent de livre autobiographique qui raconte l'histoire de l'humoriste et présentateur sud-africain.", "Autobiographie " }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTheme_ThemesId",
                table: "BookTheme",
                column: "ThemesId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_BookId",
                table: "KeyWords",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BookTheme");

            migrationBuilder.DropTable(
                name: "KeyWords");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
