using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bolgelers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolgeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolgelers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResmiTatiller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acıklama = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IslemTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResmiTatiller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sehirlers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sehir = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirlers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EczaneBilgileris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EczaneAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EczaneAdresi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EczaneTelefon = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EczaneMail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BolgelerId = table.Column<int>(type: "int", nullable: false),
                    SehirlerId = table.Column<int>(type: "int", nullable: false),
                    EczaneAdresTarifi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EczaneSahibiAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EczaneSahibiSoyadi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EczaneSahibiTC = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    EczaneSahibiTelefon = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EczaneKordinatLongitude = table.Column<double>(type: "float", nullable: false),
                    EczaneKordinatLatitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EczaneBilgileris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EczaneBilgileris_Bolgelers_BolgelerId",
                        column: x => x.BolgelerId,
                        principalTable: "Bolgelers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EczaneBilgileris_Sehirlers_SehirlerId",
                        column: x => x.SehirlerId,
                        principalTable: "Sehirlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MazeretBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EczaneBilgileriId = table.Column<int>(type: "int", nullable: false),
                    BaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acıklama = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IslemTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MazeretBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MazeretBilgileri_EczaneBilgileris_EczaneBilgileriId",
                        column: x => x.EczaneBilgileriId,
                        principalTable: "EczaneBilgileris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nobetlers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EczaneBilgileriId = table.Column<int>(type: "int", nullable: false),
                    NobetTuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acıklama = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nobetlers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nobetlers_EczaneBilgileris_EczaneBilgileriId",
                        column: x => x.EczaneBilgileriId,
                        principalTable: "EczaneBilgileris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bolgelers",
                columns: new[] { "Id", "BolgeName" },
                values: new object[,]
                {
                    { 1, "Merkez" },
                    { 2, "Alaca" }
                });

            migrationBuilder.InsertData(
                table: "Sehirlers",
                columns: new[] { "Id", "Sehir" },
                values: new object[,]
                {
                    { 1, "Çorm" },
                    { 2, "Ankara" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Editor" },
                    { 3, "User" },
                    { 4, "Visitor" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsEmailConfirmed", "Password", "UserName", "UserRoleId" },
                values: new object[] { 1, "admin@admin", true, "Admin", "Admin", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_EczaneBilgileris_BolgelerId",
                table: "EczaneBilgileris",
                column: "BolgelerId");

            migrationBuilder.CreateIndex(
                name: "IX_EczaneBilgileris_SehirlerId",
                table: "EczaneBilgileris",
                column: "SehirlerId");

            migrationBuilder.CreateIndex(
                name: "IX_MazeretBilgileri_EczaneBilgileriId",
                table: "MazeretBilgileri",
                column: "EczaneBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_Nobetlers_EczaneBilgileriId",
                table: "Nobetlers",
                column: "EczaneBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MazeretBilgileri");

            migrationBuilder.DropTable(
                name: "Nobetlers");

            migrationBuilder.DropTable(
                name: "ResmiTatiller");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EczaneBilgileris");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Bolgelers");

            migrationBuilder.DropTable(
                name: "Sehirlers");
        }
    }
}
