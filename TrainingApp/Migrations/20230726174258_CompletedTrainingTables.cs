using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrainingApp.Migrations
{
    /// <inheritdoc />
    public partial class CompletedTrainingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FixedDrills",
                columns: table => new
                {
                    FixedDrillId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedDrills", x => x.FixedDrillId);
                });

            migrationBuilder.CreateTable(
                name: "UserTraining",
                columns: table => new
                {
                    TrainingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgeGroup = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTraining", x => x.TrainingId);
                    table.ForeignKey(
                        name: "FK_UserTraining_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrainingDrills",
                columns: table => new
                {
                    UserTrainingDrillId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    DrillId = table.Column<int>(type: "integer", nullable: false),
                    UserTrainingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrainingDrills", x => x.UserTrainingDrillId);
                    table.ForeignKey(
                        name: "FK_UserTrainingDrills_FixedDrills_DrillId",
                        column: x => x.DrillId,
                        principalTable: "FixedDrills",
                        principalColumn: "FixedDrillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTrainingDrills_UserTraining_UserTrainingId",
                        column: x => x.UserTrainingId,
                        principalTable: "UserTraining",
                        principalColumn: "TrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTraining_UserId",
                table: "UserTraining",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingDrills_DrillId",
                table: "UserTrainingDrills",
                column: "DrillId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingDrills_UserTrainingId",
                table: "UserTrainingDrills",
                column: "UserTrainingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTrainingDrills");

            migrationBuilder.DropTable(
                name: "FixedDrills");

            migrationBuilder.DropTable(
                name: "UserTraining");
        }
    }
}
