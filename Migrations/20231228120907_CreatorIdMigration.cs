using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNPM_BE.Migrations
{
    /// <inheritdoc />
    public partial class CreatorIdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_service_fee_type_app_user_app_user_id",
                table: "service_fee_type");

            migrationBuilder.DropIndex(
                name: "ix_service_fee_type_creator_id",
                table: "service_fee_type");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "vehicle_type");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "service_fee_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "creator_id",
                table: "vehicle_type",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "creator_id",
                table: "service_fee_type",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_service_fee_type_creator_id",
                table: "service_fee_type",
                column: "creator_id");

            migrationBuilder.AddForeignKey(
                name: "fk_service_fee_type_app_user_app_user_id",
                table: "service_fee_type",
                column: "creator_id",
                principalTable: "app_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
