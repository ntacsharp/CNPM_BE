using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNPM_BE.Migrations
{
    /// <inheritdoc />
    public partial class FKeyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_vehicle_apartment_id",
                table: "vehicle",
                column: "apartment_id");

            migrationBuilder.AddForeignKey(
                name: "fk_vehicle_apartment_apartment_id",
                table: "vehicle",
                column: "apartment_id",
                principalTable: "apartment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vehicle_apartment_apartment_id",
                table: "vehicle");

            migrationBuilder.DropIndex(
                name: "ix_vehicle_apartment_id",
                table: "vehicle");
        }
    }
}
