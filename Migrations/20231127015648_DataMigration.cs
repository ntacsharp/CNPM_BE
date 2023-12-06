using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CNPM_BE.Migrations
{
    /// <inheritdoc />
    public partial class DataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "donation_fund",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    expiration_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    smallest_amount = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    received_amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_donation_fund", x => x.id);
                    table.ForeignKey(
                        name: "fk_donation_fund_app_user_app_user_id",
                        column: x => x.creator_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "household",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    manager_id = table.Column<int>(type: "integer", nullable: false),
                    owner_name = table.Column<string>(type: "text", nullable: false),
                    household_code = table.Column<string>(type: "text", nullable: false),
                    vehicle_count = table.Column<int>(type: "integer", nullable: false),
                    area = table.Column<double>(type: "double precision", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deactivate_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    service_fee_per_member = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_household", x => x.id);
                    table.ForeignKey(
                        name: "fk_household_app_user_app_user_id",
                        column: x => x.manager_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "current_household_fee",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    current_management_fee = table.Column<int>(type: "integer", nullable: false),
                    current_parking_fee = table.Column<int>(type: "integer", nullable: false),
                    current_service_fee = table.Column<int>(type: "integer", nullable: false),
                    leftover_management_fee = table.Column<int>(type: "integer", nullable: false),
                    leftover_parking_fee = table.Column<int>(type: "integer", nullable: false),
                    leftover_service_fee = table.Column<int>(type: "integer", nullable: false),
                    total_management_fee = table.Column<int>(type: "integer", nullable: false),
                    total_parking_fee = table.Column<int>(type: "integer", nullable: false),
                    total_service_fee = table.Column<int>(type: "integer", nullable: false),
                    paid_management_fee = table.Column<int>(type: "integer", nullable: false),
                    paid_parking_fee = table.Column<int>(type: "integer", nullable: false),
                    paid_service_fee = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_current_household_fee", x => x.id);
                    table.ForeignKey(
                        name: "fk_current_household_fee_household_household_id",
                        column: x => x.household_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "donation",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    donator_id = table.Column<int>(type: "integer", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fund_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_donation", x => x.id);
                    table.ForeignKey(
                        name: "fk_donation_donation_fund_donation_fund_id",
                        column: x => x.fund_id,
                        principalTable: "donation_fund",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_donation_household_household_id",
                        column: x => x.donator_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fee_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fee_payment", x => x.id);
                    table.ForeignKey(
                        name: "fk_fee_payment_household_household_id",
                        column: x => x.household_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "household_donation",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    donator_id = table.Column<int>(type: "integer", nullable: false),
                    fund_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_household_donation", x => x.id);
                    table.ForeignKey(
                        name: "fk_household_donation_donation_fund_donation_fund_id",
                        column: x => x.fund_id,
                        principalTable: "donation_fund",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_household_donation_household_household_id",
                        column: x => x.donator_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "household_fee_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    month = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    current_management_fee = table.Column<int>(type: "integer", nullable: false),
                    current_parking_fee = table.Column<int>(type: "integer", nullable: false),
                    current_service_fee = table.Column<int>(type: "integer", nullable: false),
                    leftover_management_fee = table.Column<int>(type: "integer", nullable: false),
                    leftover_parking_fee = table.Column<int>(type: "integer", nullable: false),
                    leftover_service_fee = table.Column<int>(type: "integer", nullable: false),
                    total_management_fee = table.Column<int>(type: "integer", nullable: false),
                    total_parking_fee = table.Column<int>(type: "integer", nullable: false),
                    total_service_fee = table.Column<int>(type: "integer", nullable: false),
                    paid_management_fee = table.Column<int>(type: "integer", nullable: false),
                    paid_parking_fee = table.Column<int>(type: "integer", nullable: false),
                    paid_service_fee = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_household_fee_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_household_fee_history_household_household_id",
                        column: x => x.household_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "household_member",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    household_id = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_household_member", x => x.id);
                    table.ForeignKey(
                        name: "fk_household_member_household_household_id",
                        column: x => x.household_id,
                        principalTable: "household",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_current_household_fee_household_id",
                table: "current_household_fee",
                column: "household_id");

            migrationBuilder.CreateIndex(
                name: "ix_donation_donator_id",
                table: "donation",
                column: "donator_id");

            migrationBuilder.CreateIndex(
                name: "ix_donation_fund_id",
                table: "donation",
                column: "fund_id");

            migrationBuilder.CreateIndex(
                name: "ix_donation_fund_creator_id",
                table: "donation_fund",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_fee_payment_household_id",
                table: "fee_payment",
                column: "household_id");

            migrationBuilder.CreateIndex(
                name: "ix_household_manager_id",
                table: "household",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "ix_household_donation_donator_id",
                table: "household_donation",
                column: "donator_id");

            migrationBuilder.CreateIndex(
                name: "ix_household_donation_fund_id",
                table: "household_donation",
                column: "fund_id");

            migrationBuilder.CreateIndex(
                name: "ix_household_fee_history_household_id",
                table: "household_fee_history",
                column: "household_id");

            migrationBuilder.CreateIndex(
                name: "ix_household_member_household_id",
                table: "household_member",
                column: "household_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "current_household_fee");

            migrationBuilder.DropTable(
                name: "donation");

            migrationBuilder.DropTable(
                name: "fee_payment");

            migrationBuilder.DropTable(
                name: "household_donation");

            migrationBuilder.DropTable(
                name: "household_fee_history");

            migrationBuilder.DropTable(
                name: "household_member");

            migrationBuilder.DropTable(
                name: "donation_fund");

            migrationBuilder.DropTable(
                name: "household");
        }
    }
}
