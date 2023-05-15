using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMvcBiblio.Data.Migrations
{
    public partial class BibliothequeReaderMVC1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Phone" },
                values: new object[] { 1, "Ermane.Pourmohtasham@mail.com", "Ermane", "Pourmohtasham", "0606060606" });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Phone" },
                values: new object[] { 2, "Jean.Valjean@mail.com", "Jean", "Valjean", "0676869616" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Readers");
        }
    }
}
