using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace agriWeatherTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSignalGeneratedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignalsGenerated_Crops_CropId",
                table: "SignalsGenerated");

            migrationBuilder.DropForeignKey(
                name: "FK_SignalsGenerated_HealthScores_HealthScoreId",
                table: "SignalsGenerated");

            migrationBuilder.DropForeignKey(
                name: "FK_SignalsGenerated_Locations_LocationId",
                table: "SignalsGenerated");

            migrationBuilder.DropForeignKey(
                name: "FK_Weathers_SignalsGenerated_SignalGeneratedId",
                table: "Weathers");

            migrationBuilder.DropIndex(
                name: "IX_Weathers_SignalGeneratedId",
                table: "Weathers");

            migrationBuilder.DropIndex(
                name: "IX_SignalsGenerated_CropId",
                table: "SignalsGenerated");

            migrationBuilder.DropIndex(
                name: "IX_SignalsGenerated_LocationId",
                table: "SignalsGenerated");

            migrationBuilder.DropColumn(
                name: "SignalGeneratedId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "SignalGeneratedId",
                table: "Crops");

            migrationBuilder.CreateIndex(
                name: "IX_SignalsGenerated_CropId",
                table: "SignalsGenerated",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_SignalsGenerated_LocationId",
                table: "SignalsGenerated",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SignalsGenerated_WeatherId",
                table: "SignalsGenerated",
                column: "WeatherId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SignalsGenerated_Crops_CropId",
                table: "SignalsGenerated",
                column: "CropId",
                principalTable: "Crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SignalsGenerated_HealthScores_HealthScoreId",
                table: "SignalsGenerated",
                column: "HealthScoreId",
                principalTable: "HealthScores",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SignalsGenerated_Locations_LocationId",
                table: "SignalsGenerated",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SignalsGenerated_Weathers_WeatherId",
                table: "SignalsGenerated",
                column: "WeatherId",
                principalTable: "Weathers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignalsGenerated_Crops_CropId",
                table: "SignalsGenerated");

            migrationBuilder.DropForeignKey(
                name: "FK_SignalsGenerated_HealthScores_HealthScoreId",
                table: "SignalsGenerated");

            migrationBuilder.DropForeignKey(
                name: "FK_SignalsGenerated_Locations_LocationId",
                table: "SignalsGenerated");

            migrationBuilder.DropForeignKey(
                name: "FK_SignalsGenerated_Weathers_WeatherId",
                table: "SignalsGenerated");

            migrationBuilder.DropIndex(
                name: "IX_SignalsGenerated_CropId",
                table: "SignalsGenerated");

            migrationBuilder.DropIndex(
                name: "IX_SignalsGenerated_LocationId",
                table: "SignalsGenerated");

            migrationBuilder.DropIndex(
                name: "IX_SignalsGenerated_WeatherId",
                table: "SignalsGenerated");

            migrationBuilder.AddColumn<int>(
                name: "SignalGeneratedId",
                table: "Locations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SignalGeneratedId",
                table: "Crops",
                type: "integer",
                nullable: true);

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
                name: "IX_SignalsGenerated_LocationId",
                table: "SignalsGenerated",
                column: "LocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SignalsGenerated_Crops_CropId",
                table: "SignalsGenerated",
                column: "CropId",
                principalTable: "Crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SignalsGenerated_HealthScores_HealthScoreId",
                table: "SignalsGenerated",
                column: "HealthScoreId",
                principalTable: "HealthScores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SignalsGenerated_Locations_LocationId",
                table: "SignalsGenerated",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Weathers_SignalsGenerated_SignalGeneratedId",
                table: "Weathers",
                column: "SignalGeneratedId",
                principalTable: "SignalsGenerated",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
