﻿// <auto-generated />
using System;
using CNPM_BE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CNPM_BE.Migrations
{
    [DbContext(typeof(CNPMDbContext))]
    partial class CNPMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CNPM_BE.Models.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApartmentCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("apartment_code");

                    b.Property<double>("Area")
                        .HasColumnType("double precision")
                        .HasColumnName("area");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("owner_id");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("position");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.Property<int>("RoomCount")
                        .HasColumnType("integer")
                        .HasColumnName("room_count");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_apartment");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_apartment_creator_id");

                    b.ToTable("apartment", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bank_account_number");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bank_name");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FacebookLink")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("facebook_link");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_salt");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.Property<string>("ZaloLink")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("zalo_link");

                    b.HasKey("Id")
                        .HasName("pk_app_user");

                    b.ToTable("app_user", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.Contribution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Charity")
                        .HasColumnType("integer")
                        .HasColumnName("charity");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_time");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<int>("DGFestival")
                        .HasColumnType("integer")
                        .HasColumnName("dg_festival");

                    b.Property<int>("ForChildren")
                        .HasColumnType("integer")
                        .HasColumnName("for_children");

                    b.Property<int>("ForTheElderly")
                        .HasColumnType("integer")
                        .HasColumnName("for_the_elderly");

                    b.Property<int>("ForThePoor")
                        .HasColumnType("integer")
                        .HasColumnName("for_the_poor");

                    b.Property<int>("ForVNSeasAndIslands")
                        .HasColumnType("integer")
                        .HasColumnName("for_vn_seas_and_islands");

                    b.Property<int>("Gratitude")
                        .HasColumnType("integer")
                        .HasColumnName("gratitude");

                    b.Property<int>("ResidentId")
                        .HasColumnType("integer")
                        .HasColumnName("resident_id");

                    b.Property<int>("ResidentialGroup")
                        .HasColumnType("integer")
                        .HasColumnName("residential_group");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("StudyPromotion")
                        .HasColumnType("integer")
                        .HasColumnName("study_promotion");

                    b.HasKey("Id")
                        .HasName("pk_contribution");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_contribution_creator_id");

                    b.HasIndex("ResidentId")
                        .HasDatabaseName("ix_contribution_resident_id");

                    b.ToTable("contribution", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.Fee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApartmentId")
                        .HasColumnType("integer")
                        .HasColumnName("apartment_id");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_time");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expired_date");

                    b.Property<int>("ManagementFee")
                        .HasColumnType("integer")
                        .HasColumnName("management_fee");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("note");

                    b.Property<int>("ParkingFee")
                        .HasColumnType("integer")
                        .HasColumnName("parking_fee");

                    b.Property<int>("ReceivedAmount")
                        .HasColumnType("integer")
                        .HasColumnName("received_amount");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_fee");

                    b.HasIndex("ApartmentId")
                        .HasDatabaseName("ix_fee_apartment_id");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_fee_creator_id");

                    b.ToTable("fee", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.FeePayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_time");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<int>("FeeId")
                        .HasColumnType("integer")
                        .HasColumnName("fee_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_fee_payment");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_fee_payment_creator_id");

                    b.HasIndex("FeeId")
                        .HasDatabaseName("ix_fee_payment_fee_id");

                    b.ToTable("fee_payment", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.Resident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApartmentId")
                        .HasColumnType("integer")
                        .HasColumnName("apartment_id");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cccd");

                    b.Property<string>("Career")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("career");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_time");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_time");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<bool>("IsOwner")
                        .HasColumnType("boolean")
                        .HasColumnName("is_owner");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("ResidentCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("resident_code");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_resident");

                    b.HasIndex("ApartmentId")
                        .HasDatabaseName("ix_resident_apartment_id");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_resident_creator_id");

                    b.ToTable("resident", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.ServiceFee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<int>("FeeId")
                        .HasColumnType("integer")
                        .HasColumnName("fee_id");

                    b.Property<int>("NewCount")
                        .HasColumnType("integer")
                        .HasColumnName("new_count");

                    b.Property<int>("OldCount")
                        .HasColumnType("integer")
                        .HasColumnName("old_count");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("TotalFee")
                        .HasColumnType("integer")
                        .HasColumnName("total_fee");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer")
                        .HasColumnName("type_id");

                    b.HasKey("Id")
                        .HasName("pk_service_fee");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_service_fee_creator_id");

                    b.HasIndex("FeeId")
                        .HasDatabaseName("ix_service_fee_fee_id");

                    b.HasIndex("TypeId")
                        .HasDatabaseName("ix_service_fee_type_id");

                    b.ToTable("service_fee", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.ServiceFeeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<int>("MeasuringUnit")
                        .HasColumnType("integer")
                        .HasColumnName("measuring_unit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("PricePerUnit")
                        .HasColumnType("integer")
                        .HasColumnName("price_per_unit");

                    b.Property<string>("ServiceFeeTypeCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("service_fee_type_code");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_service_fee_type");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_service_fee_type_creator_id");

                    b.ToTable("service_fee_type", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApartmentId")
                        .HasColumnType("integer")
                        .HasColumnName("apartment_id");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_time");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("owner_id");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("plate");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("VehicleCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("vehicle_code");

                    b.Property<int>("VehicleTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("vehicle_type_id");

                    b.HasKey("Id")
                        .HasName("pk_vehicle");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_vehicle_creator_id");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_vehicle_owner_id");

                    b.HasIndex("VehicleTypeId")
                        .HasDatabaseName("ix_vehicle_vehicle_type_id");

                    b.ToTable("vehicle", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.VehicleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer")
                        .HasColumnName("creator_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("ParkingFee")
                        .HasColumnType("integer")
                        .HasColumnName("parking_fee");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("VehicleTypeCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("vehicle_type_code");

                    b.HasKey("Id")
                        .HasName("pk_vehicle_type");

                    b.ToTable("vehicle_type", (string)null);
                });

            modelBuilder.Entity("CNPM_BE.Models.Apartment", b =>
                {
                    b.HasOne("CNPM_BE.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_apartment_app_user_app_user_id");
                });

            modelBuilder.Entity("CNPM_BE.Models.Contribution", b =>
                {
                    b.HasOne("CNPM_BE.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_contribution_app_user_app_user_id");

                    b.HasOne("CNPM_BE.Models.Resident", null)
                        .WithMany()
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_contribution_resident_resident_id");
                });

            modelBuilder.Entity("CNPM_BE.Models.Fee", b =>
                {
                    b.HasOne("CNPM_BE.Models.Apartment", null)
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_fee_apartment_apartment_id");

                    b.HasOne("CNPM_BE.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_fee_app_user_app_user_id");
                });

            modelBuilder.Entity("CNPM_BE.Models.FeePayment", b =>
                {
                    b.HasOne("CNPM_BE.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_fee_payment_app_user_app_user_id");

                    b.HasOne("CNPM_BE.Models.Fee", null)
                        .WithMany()
                        .HasForeignKey("FeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_fee_payment_fee_fee_id");
                });

            modelBuilder.Entity("CNPM_BE.Models.Resident", b =>
                {
                    b.HasOne("CNPM_BE.Models.Apartment", null)
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_resident_apartment_apartment_id");

                    b.HasOne("CNPM_BE.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_resident_app_user_app_user_id");
                });

            modelBuilder.Entity("CNPM_BE.Models.ServiceFee", b =>
                {
                    b.HasOne("CNPM_BE.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_service_fee_app_user_app_user_id");

                    b.HasOne("CNPM_BE.Models.Fee", null)
                        .WithMany()
                        .HasForeignKey("FeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_service_fee_fee_fee_id");

                    b.HasOne("CNPM_BE.Models.ServiceFeeType", null)
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_service_fee_service_fee_type_service_fee_type_id");
                });

            modelBuilder.Entity("CNPM_BE.Models.ServiceFeeType", b =>
                {
                    b.HasOne("CNPM_BE.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_service_fee_type_app_user_app_user_id");
                });

            modelBuilder.Entity("CNPM_BE.Models.Vehicle", b =>
                {
                    b.HasOne("CNPM_BE.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_vehicle_app_user_app_user_id");

                    b.HasOne("CNPM_BE.Models.Resident", null)
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_vehicle_resident_resident_id");

                    b.HasOne("CNPM_BE.Models.VehicleType", null)
                        .WithMany()
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_vehicle_vehicle_type_vehicle_type_id");
                });
#pragma warning restore 612, 618
        }
    }
}
