using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNPM_BE.Migrations
{
    /// <inheritdoc />
    public partial class ContributionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contribution_apartment_apartment_id",
                table: "contribution");

            migrationBuilder.DropIndex(
                name: "ix_contribution_apartment_id",
                table: "contribution");

            migrationBuilder.RenameColumn(
                name: "apartment_id",
                table: "contribution",
                newName: "status");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_time",
                table: "contribution",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "resident_id",
                table: "contribution",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_contribution_resident_id",
                table: "contribution",
                column: "resident_id");

            migrationBuilder.AddForeignKey(
                name: "fk_contribution_resident_resident_id",
                table: "contribution",
                column: "resident_id",
                principalTable: "resident",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contribution_resident_resident_id",
                table: "contribution");

            migrationBuilder.DropIndex(
                name: "ix_contribution_resident_id",
                table: "contribution");

            migrationBuilder.DropColumn(
                name: "created_time",
                table: "contribution");

            migrationBuilder.DropColumn(
                name: "resident_id",
                table: "contribution");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "contribution",
                newName: "apartment_id");

            migrationBuilder.CreateIndex(
                name: "ix_contribution_apartment_id",
                table: "contribution",
                column: "apartment_id");

            migrationBuilder.AddForeignKey(
                name: "fk_contribution_apartment_apartment_id",
                table: "contribution",
                column: "apartment_id",
                principalTable: "apartment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
