using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace agriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConditionThresholds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MinTemperature = table.Column<double>(type: "double precision", nullable: false),
                    MaxTemperature = table.Column<double>(type: "double precision", nullable: false),
                    MildMinTemp = table.Column<double>(type: "double precision", nullable: false),
                    MildMaxTemp = table.Column<double>(type: "double precision", nullable: false),
                    MildResilienceDuration = table.Column<int>(type: "integer", nullable: false),
                    ModerateMinTemp = table.Column<double>(type: "double precision", nullable: false),
                    ModerateMaxTemp = table.Column<double>(type: "double precision", nullable: false),
                    ModerateResilienceDuration = table.Column<int>(type: "integer", nullable: false),
                    SevereMinTemp = table.Column<double>(type: "double precision", nullable: false),
                    SevereMaxTemp = table.Column<double>(type: "double precision", nullable: false),
                    SevereResilienceDuration = table.Column<int>(type: "integer", nullable: false),
                    ExtremeMinTemp = table.Column<double>(type: "double precision", nullable: false),
                    ExtremeMaxTemp = table.Column<double>(type: "double precision", nullable: false),
                    ExtremeResilienceDuration = table.Column<int>(type: "integer", nullable: false),
                    OptimalHumidity = table.Column<double>(type: "double precision", nullable: false),
                    MinHumidity = table.Column<double>(type: "double precision", nullable: false),
                    MaxHumidity = table.Column<double>(type: "double precision", nullable: false),
                    MinRainfall = table.Column<double>(type: "double precision", nullable: false),
                    MaxRainfall = table.Column<double>(type: "double precision", nullable: false),
                    MaxWindSpeed = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionThresholds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrowthCycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CropId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthCycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrowthCycles_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    CropId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GrowthStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StageName = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OptimalConditionsId = table.Column<int>(type: "integer", nullable: false),
                    AdverseConditionsId = table.Column<int>(type: "integer", nullable: false),
                    ResilienceDurationInDays = table.Column<int>(type: "integer", nullable: false),
                    HistoricalAdverseImpactScore = table.Column<double>(type: "double precision", nullable: false),
                    GrowthCycleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrowthStages_ConditionThresholds_AdverseConditionsId",
                        column: x => x.AdverseConditionsId,
                        principalTable: "ConditionThresholds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrowthStages_ConditionThresholds_OptimalConditionsId",
                        column: x => x.OptimalConditionsId,
                        principalTable: "ConditionThresholds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrowthStages_GrowthCycles_GrowthCycleId",
                        column: x => x.GrowthCycleId,
                        principalTable: "GrowthCycles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HealthScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CropId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Score = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthScores_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthScores_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    Humidity = table.Column<double>(type: "double precision", nullable: false),
                    Rainfall = table.Column<double>(type: "double precision", nullable: false),
                    WindSpeed = table.Column<double>(type: "double precision", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weathers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrowthCycles_CropId",
                table: "GrowthCycles",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthStages_AdverseConditionsId",
                table: "GrowthStages",
                column: "AdverseConditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthStages_GrowthCycleId",
                table: "GrowthStages",
                column: "GrowthCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthStages_OptimalConditionsId",
                table: "GrowthStages",
                column: "OptimalConditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthScores_CropId",
                table: "HealthScores",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthScores_LocationId",
                table: "HealthScores",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CropId",
                table: "Locations",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_LocationId",
                table: "Weathers",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrowthStages");

            migrationBuilder.DropTable(
                name: "HealthScores");

            migrationBuilder.DropTable(
                name: "Weathers");

            migrationBuilder.DropTable(
                name: "ConditionThresholds");

            migrationBuilder.DropTable(
                name: "GrowthCycles");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Crops");
        }
    }
}
