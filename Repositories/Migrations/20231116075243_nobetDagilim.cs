using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class nobetDagilim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NobetTuru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NobetTuru = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NobetTuru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NobetDagilim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EczaneId = table.Column<int>(type: "int", nullable: false),
                    YasaklıEczaneId = table.Column<int>(type: "int", nullable: false),
                    NobetTuruId = table.Column<int>(type: "int", nullable: false),
                    NobetSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NobetDagilim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NobetDagilim_NobetTuru_NobetTuruId",
                        column: x => x.NobetTuruId,
                        principalTable: "NobetTuru",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NobetTuru",
                columns: new[] { "Id", "NobetTuru" },
                values: new object[,]
                {
                    { 1, "Resmi Tatil" },
                    { 2, "Cumartesi" },
                    { 3, "Pazar" },
                    { 4, "Haftaici" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NobetDagilim_NobetTuruId",
                table: "NobetDagilim",
                column: "NobetTuruId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NobetDagilim");

            migrationBuilder.DropTable(
                name: "NobetTuru");
        }
    }
}
