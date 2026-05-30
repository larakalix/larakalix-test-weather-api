using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace car.Api.Migrations
{
    /// <inheritdoc />
    public partial class initmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarcasAutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Pais = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasAutos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MarcasAutos",
                columns: new[] { "Id", "Nombre", "Pais" },
                values: new object[,]
                {
                    { 1, "Toyota", "Japan" },
                    { 2, "Ford", "United States" },
                    { 3, "BMW", "Germany" },
                    { 4, "Mercedes-Benz", "Germany" },
                    { 5, "Honda", "Japan" },
                    { 6, "Nissan", "Japan" },
                    { 7, "Chevrolet", "United States" },
                    { 8, "Hyundai", "South Korea" },
                    { 9, "Kia", "South Korea" },
                    { 10, "Volkswagen", "Germany" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarcasAutos");
        }
    }
}
