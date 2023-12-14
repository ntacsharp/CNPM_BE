using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CNPM_BE.Migrations
{
    /// <inheritdoc />
    public partial class FeeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "creator_id",
                table: "vehicle_type",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "vehicle_type",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "vehicle_type_code",
                table: "vehicle_type",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "apartment_id",
                table: "vehicle",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "contribution",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    apartment_id = table.Column<int>(type: "integer", nullable: false),
                    for_the_poor = table.Column<int>(type: "integer", nullable: false),
                    for_vn_seas_and_islands = table.Column<int>(type: "integer", nullable: false),
                    dg_festival = table.Column<int>(type: "integer", nullable: false),
                    residential_group = table.Column<int>(type: "integer", nullable: false),
                    for_children = table.Column<int>(type: "integer", nullable: false),
                    charity = table.Column<int>(type: "integer", nullable: false),
                    gratitude = table.Column<int>(type: "integer", nullable: false),
                    study_promotion = table.Column<int>(type: "integer", nullable: false),
                    for_the_elderly = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contribution", x => x.id);
                    table.ForeignKey(
                        name: "fk_contribution_apartment_apartment_id",
                        column: x => x.apartment_id,
                        principalTable: "apartment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contribution_app_user_app_user_id",
                        column: x => x.creator_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fee",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    apartment_id = table.Column<int>(type: "integer", nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expired_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    management_fee = table.Column<int>(type: "integer", nullable: false),
                    parking_fee = table.Column<int>(type: "integer", nullable: false),
                    received_amount = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fee", x => x.id);
                    table.ForeignKey(
                        name: "fk_fee_apartment_apartment_id",
                        column: x => x.apartment_id,
                        principalTable: "apartment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fee_app_user_app_user_id",
                        column: x => x.creator_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "service_fee_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    service_fee_type_code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    price_per_unit = table.Column<int>(type: "integer", nullable: false),
                    measuring_unit = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_fee_type", x => x.id);
                    table.ForeignKey(
                        name: "fk_service_fee_type_app_user_app_user_id",
                        column: x => x.creator_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fee_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fee_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fee_payment", x => x.id);
                    table.ForeignKey(
                        name: "fk_fee_payment_app_user_app_user_id",
                        column: x => x.creator_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fee_payment_fee_fee_id",
                        column: x => x.fee_id,
                        principalTable: "fee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "service_fee",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    old_count = table.Column<int>(type: "integer", nullable: false),
                    new_count = table.Column<int>(type: "integer", nullable: false),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    fee_id = table.Column<int>(type: "integer", nullable: false),
                    total_fee = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_fee", x => x.id);
                    table.ForeignKey(
                        name: "fk_service_fee_app_user_app_user_id",
                        column: x => x.creator_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_service_fee_fee_fee_id",
                        column: x => x.fee_id,
                        principalTable: "fee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_service_fee_service_fee_type_service_fee_type_id",
                        column: x => x.type_id,
                        principalTable: "service_fee_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_contribution_apartment_id",
                table: "contribution",
                column: "apartment_id");

            migrationBuilder.CreateIndex(
                name: "ix_contribution_creator_id",
                table: "contribution",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_fee_apartment_id",
                table: "fee",
                column: "apartment_id");

            migrationBuilder.CreateIndex(
                name: "ix_fee_creator_id",
                table: "fee",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_fee_payment_creator_id",
                table: "fee_payment",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_fee_payment_fee_id",
                table: "fee_payment",
                column: "fee_id");

            migrationBuilder.CreateIndex(
                name: "ix_service_fee_creator_id",
                table: "service_fee",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_service_fee_fee_id",
                table: "service_fee",
                column: "fee_id");

            migrationBuilder.CreateIndex(
                name: "ix_service_fee_type_id",
                table: "service_fee",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_service_fee_type_creator_id",
                table: "service_fee_type",
                column: "creator_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contribution");

            migrationBuilder.DropTable(
                name: "fee_payment");

            migrationBuilder.DropTable(
                name: "service_fee");

            migrationBuilder.DropTable(
                name: "fee");

            migrationBuilder.DropTable(
                name: "service_fee_type");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "vehicle_type");

            migrationBuilder.DropColumn(
                name: "status",
                table: "vehicle_type");

            migrationBuilder.DropColumn(
                name: "vehicle_type_code",
                table: "vehicle_type");

            migrationBuilder.DropColumn(
                name: "apartment_id",
                table: "vehicle");
        }
    }
}
