using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EfcDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Domain = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    SecurityLevel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LocationData = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FieldId = table.Column<int>(type: "INTEGER", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    MoistureLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    soiltype = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Domain", "Email", "Name", "Password", "Role", "SecurityLevel", "Username" },
                values: new object[,]
                {
                    { 1, 30, "Domain1", "user1@example.com", "User One", "1234", "Admin", 1, "Rolf" },
                    { 2, 25, "Domain2", "user2@example.com", "User Two", "1234", "User", 2, "Alecsen" },
                    { 3, 28, "Domain3", "user3@example.com", "User Three", "1234", "User", 2, "Maria" },
                    { 4, 32, "Domain4", "user4@example.com", "User Four", "1234", "Manager", 3, "Røde" },
                    { 5, 35, "Domain5", "user5@example.com", "User Five", "1234", "Manager", 3, "user5" }
                });

            migrationBuilder.InsertData(
                table: "Fields",
                columns: new[] { "Id", "LocationData", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, "(-100.123, 50.456), (-100.789, 50.456), (-100.789, 50.123), (-100.123, 50.123)", "RolfMark1", 1 },
                    { 2, "(-101.123, 51.456), (-101.789, 51.456), (-101.789, 51.123), (-101.123, 51.123)", "RolfMark2", 1 },
                    { 3, "(-102.123, 52.456), (-102.789, 52.456), (-102.789, 52.123), (-102.123, 52.123)", "AlecsenMark1", 2 },
                    { 4, "(-103.123, 53.456), (-103.789, 53.456), (-103.789, 53.123), (-103.123, 53.123)", "AlecsenMark2", 2 },
                    { 5, "(-104.123, 54.456), (-104.789, 54.456), (-104.789, 54.123), (-104.123, 54.123)", "MariasMark1", 3 }
                });

            migrationBuilder.InsertData(
                table: "Sensors",
                columns: new[] { "Id", "FieldId", "Latitude", "Longitude", "MoistureLevel", "soiltype" },
                values: new object[,]
                {
                    { 1, 1, 50.299999999999997, -100.3, 50, 0 },
                    { 2, 1, 50.399999999999999, -100.3, 55, 0 },
                    { 3, 2, 51.299999999999997, -100.5, 60, 0 },
                    { 4, 2, 51.299999999999997, -100.3, 65, 0 },
                    { 5, 3, 52.399999999999999, -102.2, 50, 0 },
                    { 6, 3, 52.399999999999999, -102.40000000000001, 55, 0 },
                    { 7, 4, 53.399999999999999, -103.2, 60, 0 },
                    { 8, 4, 53.399999999999999, -103.40000000000001, 65, 0 },
                    { 9, 5, 54.399999999999999, -104.2, 50, 0 },
                    { 10, 5, 54.399999999999999, -104.40000000000001, 55, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_OwnerId",
                table: "Fields",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_FieldId",
                table: "Sensors",
                column: "FieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
