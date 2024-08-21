using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace agriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class added_signalsGenerated_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SignalGeneratedId",
                table: "Weathers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SignalGeneratedId",
                table: "Locations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SignalGeneratedId",
                table: "HealthScores",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SignalGeneratedId",
                table: "Crops",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SignalsGenerated",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CropId = table.Column<int>(type: "integer", nullable: false),
                    HealthScoreId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    CommodityPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SignalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Temperature = table.Column<decimal>(type: "numeric", nullable: false),
                    WeatherId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<decimal>(type: "numeric", nullable: false),
                    SignalType = table.Column<string>(type: "text", nullable: false),
                    IsRealized = table.Column<bool>(type: "boolean", nullable: false),
                    AdditionalMetadata = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignalsGenerated", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignalsGenerated_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignalsGenerated_HealthScores_HealthScoreId",
                        column: x => x.HealthScoreId,
                        principalTable: "HealthScores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SignalsGenerated_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_SignalGeneratedId",
                table: "Weathers",
                column: "SignalGeneratedId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SignalsGenerated_CropId",
                table: "SignalsGenerated",
                column: "CropId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SignalsGenerated_HealthScoreId",
                table: "SignalsGenerated",
                column: "HealthScoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SignalsGenerated_LocationId",
                table: "SignalsGenerated",
                column: "LocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Weathers_SignalsGenerated_SignalGeneratedId",
                table: "Weathers",
                column: "SignalGeneratedId",
                principalTable: "SignalsGenerated",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weathers_SignalsGenerated_SignalGeneratedId",
                table: "Weathers");

            migrationBuilder.DropTable(
                name: "SignalsGenerated");

            migrationBuilder.DropIndex(
                name: "IX_Weathers_SignalGeneratedId",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "SignalGeneratedId",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "SignalGeneratedId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "SignalGeneratedId",
                table: "HealthScores");

            migrationBuilder.DropColumn(
                name: "SignalGeneratedId",
                table: "Crops");
        }
    }
}
