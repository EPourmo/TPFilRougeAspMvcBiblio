using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMvcBiblio.Data.Migrations
{
    public partial class BibliothequeReaderMVC2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookTheme",
                columns: new[] { "BooksId", "ThemesId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookTheme",
                keyColumns: new[] { "BooksId", "ThemesId" },
                keyValues: new object[] { 1, 1 });
        }
    }
}
