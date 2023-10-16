using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dogshouseservice.API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tail_length = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Color", "Name", "Tail_length", "Weight" },
                values: new object[,]
                {
                    { new Guid("284b3048-3da7-477f-93df-44ec48951399"), "grey", "Jerry", 12, 15 },
                    { new Guid("32369c97-b9a1-4810-8b64-1ab5922d8cd7"), "black&white", "Jessy", 7, 14 },
                    { new Guid("54c986fb-0d40-4e99-b6c7-a7134f55f081"), "brown&white", "Tom", 12, 41 },
                    { new Guid("e16b336b-0797-47e2-ba77-04e7aef98f35"), "red&amber", "Neo", 22, 32 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dogs");
        }
    }
}