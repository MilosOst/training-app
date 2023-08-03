using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainingApp.Migrations
{
    /// <inheritdoc />
    public partial class FixedDrillList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FixedDrills",
                columns: new[] { "FixedDrillId", "category", "name" },
                values: new object[,]
                {
                    { 1, "Defense", "Slide and Closeout" },
                    { 2, "Defense", "1/4 Court Closeout and Slide" },
                    { 3, "Defense", "Full Court Zig-Zags" },
                    { 4, "Defense", "Lane to Lane Slides" },
                    { 5, "Defense", "1v1 Deny" },
                    { 6, "Defense", "2v2 Deny" },
                    { 7, "Defense", "3v3 Deny" },
                    { 8, "Defense", "4v4 Deny" },
                    { 9, "Defense", "Tunnel Drill" },
                    { 10, "Defense", "1v1 Half Court" },
                    { 11, "Transition", "Rebound and Outlet" },
                    { 12, "Transition", "2v1 Attack" },
                    { 13, "Transition", "3v2 Attack" },
                    { 14, "Transition", "4v4 Line Touch" },
                    { 15, "Transition", "Miami Heat" },
                    { 16, "Transition", "5v3 Transition" },
                    { 17, "Passing", "Entry Pass" },
                    { 18, "Passing", "Pass to Cutter" },
                    { 19, "Passing", "Pass to Cutter Extra DEF" },
                    { 20, "Passing", "Ontario Passing" },
                    { 21, "Passing", "Fishhook" },
                    { 22, "Passing", "Italian Passing" },
                    { 23, "Passing", "3v3 Drive and Pitch" },
                    { 24, "Passing", "5v5 - No Dribble" },
                    { 25, "Passing", "4 Corner Passing" },
                    { 26, "Screening", "On Ball Defense" },
                    { 27, "Screening", "Off Ball Defense" },
                    { 28, "Screening", "Back Screens" },
                    { 29, "Screening", "Flex Screens" },
                    { 30, "Screening", "1v1 Ball Screen w/ Blocker" },
                    { 31, "Screening", "3v3 Ball Screen" },
                    { 32, "Screening", "4v4 Ball Screen" },
                    { 33, "Screening", "Hitman Drill" },
                    { 34, "DecisionMaking", "4v4 Half Court Trap" },
                    { 35, "DecisionMaking", "4v3 Rebound and Transition" },
                    { 36, "Sets", "OFF Plays" },
                    { 37, "Sets", "DEF Sets" },
                    { 38, "Sets", "Out of Bounds" },
                    { 39, "Sets", "Pressbreak" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "FixedDrills",
                keyColumn: "FixedDrillId",
                keyValue: 39);
        }
    }
}
