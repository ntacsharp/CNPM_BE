using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNPM_BE.Migrations
{
    /// <inheritdoc />
    public partial class VehicleNameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "vehicle_code",
                table: "vehicle");

            migrationBuilder.AddColumn<bool>(
                name: "is_system",
                table: "service_fee_type",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_system",
                table: "service_fee_type");

            migrationBuilder.AddColumn<string>(
                name: "vehicle_code",
                table: "vehicle",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
