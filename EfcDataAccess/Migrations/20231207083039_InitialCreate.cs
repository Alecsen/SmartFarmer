﻿using System;
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
                name: "IrrigationMachines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    FieldId = table.Column<int>(type: "INTEGER", nullable: false),
                    WaterAmount = table.Column<double>(type: "REAL", nullable: false),
                    IsRunning = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrrigationMachines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Sex = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FieldId = table.Column<int>(type: "INTEGER", nullable: false),
                    WindDirection = table.Column<string>(type: "TEXT", nullable: false),
                    WindSpeed = table.Column<double>(type: "REAL", nullable: false),
                    Precipitation = table.Column<double>(type: "REAL", nullable: false),
                    Evaporation = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CropType = table.Column<string>(type: "TEXT", nullable: false),
                    FieldCapacity = table.Column<double>(type: "REAL", nullable: false),
                    SoilType = table.Column<int>(type: "INTEGER", nullable: false),
                    MoistureLevel = table.Column<double>(type: "REAL", nullable: false),
                    LocationData = table.Column<string>(type: "TEXT", nullable: false),
                    Area = table.Column<double>(type: "REAL", nullable: true)
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

            migrationBuilder.InsertData(
                table: "IrrigationMachines",
                columns: new[] { "Id", "FieldId", "IsRunning", "OwnerId", "WaterAmount" },
                values: new object[,]
                {
                    { 1, 1, false, 1, 600.0 },
                    { 2, 2, false, 1, 600.0 },
                    { 3, 3, false, 2, 750.0 },
                    { 4, 4, false, 2, 850.0 },
                    { 5, 5, false, 3, 600.0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Birthday", "Email", "Name", "Password", "Phone", "Role", "Sex", "Username" },
                values: new object[,]
                {
                    { 1, "Hallssti 29", new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1@example.com", "User One", "1234", "53299870", "Admin", "male", "Rolf" },
                    { 2, "Hallssti 29", new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2@example.com", "User Two", "1234", "53299870", "User", "male", "Alecsen" },
                    { 3, "Hallssti 29", new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "user3@example.com", "User Three", "1234", "53299870", "User", "male", "Maria" },
                    { 4, "Hallssti 29", new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "user4@example.com", "User Four", "1234", "53299870", "Manager", "male", "Røde" },
                    { 5, "Hallssti 29", new DateTime(1998, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "user5@example.com", "User Five", "1234", "53299870", "Manager", "male", "user5" }
                });

            migrationBuilder.InsertData(
                table: "WeatherStations",
                columns: new[] { "Id", "Evaporation", "FieldId", "Precipitation", "WindDirection", "WindSpeed" },
                values: new object[,]
                {
                    { 1, 0.0, 1, 2.5, "Syd", 4.0 },
                    { 2, -1.0, 2, 1.0, "Nord", 2.0 },
                    { 3, -4.0, 3, 0.0, "Syd", 6.0 },
                    { 4, 0.0, 4, 2.5, "Vest", 8.0 }
                });

            migrationBuilder.InsertData(
                table: "Fields",
                columns: new[] { "Id", "Area", "CropType", "FieldCapacity", "LocationData", "MoistureLevel", "Name", "OwnerId", "SoilType" },
                values: new object[,]
                {
                    { 1, 255.0, "Wheat", 10.0, "(-100.123, 50.456), (-100.789, 50.456), (-100.789, 50.123), (-100.123, 50.123)", 2.5, "RolfMark1", 1, 4 },
                    { 2, 255.0, "Barly", 10.0, "(-101.123, 51.456), (-101.789, 51.456), (-101.789, 51.123), (-101.123, 51.123)", 2.5, "RolfMark2", 1, 4 },
                    { 3, 255.0, "Soybeans", 10.0, "(-102.123, 52.456), (-102.789, 52.456), (-102.789, 52.123), (-102.123, 52.123)", 2.5, "AlecsenMark1", 2, 4 },
                    { 4, 255.0, "Oat", 10.0, "(-103.123, 53.456), (-103.789, 53.456), (-103.789, 53.123), (-103.123, 53.123)", 2.5, "AlecsenMark2", 2, 4 },
                    { 5, 255.0, "Wheat", 10.0, "(-104.123, 54.456), (-104.789, 54.456), (-104.789, 54.123), (-104.123, 54.123)", 2.5, "MariasMark1", 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_OwnerId",
                table: "Fields",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "IrrigationMachines");

            migrationBuilder.DropTable(
                name: "WeatherStations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
