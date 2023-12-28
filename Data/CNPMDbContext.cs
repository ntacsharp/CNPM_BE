using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM_BE.Data
{
    public class CNPMDbContext : DbContext
    {
        public CNPMDbContext(DbContextOptions<CNPMDbContext> options) : base(options) { }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Apartment> Apartment { get; set; }
        public DbSet<Resident> Resident { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Fee> Fee { get; set; }
        public DbSet<ServiceFee> ServiceFee { get; set; }
        public DbSet<ServiceFeeType> ServiceFeeType { get; set; }
        public DbSet<FeePayment> FeePayment { get; set; }
        public DbSet<Contribution> Contribution { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSnakeCaseNamingConvention();
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Apartment>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<Resident>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<Resident>()
                .HasOne<Apartment>()
                .WithMany()
                .HasForeignKey(x => x.ApartmentId)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne<Resident>()
                .WithMany()
                .HasForeignKey(x => x.OwnerId)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne<VehicleType>()
                .WithMany()
                .HasForeignKey(x => x.VehicleTypeId)
                .IsRequired();
            modelBuilder.Entity<ServiceFee>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<ServiceFee>()
                .HasOne<ServiceFeeType>()
                .WithMany()
                .HasForeignKey(x => x.TypeId)
                .IsRequired();
            modelBuilder.Entity<ServiceFee>()
                .HasOne<Fee>()
                .WithMany()
                .HasForeignKey(x => x.FeeId)
                .IsRequired();
            modelBuilder.Entity<Fee>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<Fee>()
                .HasOne<Apartment>()
                .WithMany()
                .HasForeignKey(x => x.ApartmentId)
                .IsRequired();
            modelBuilder.Entity<Contribution>()
                .HasOne<Resident>()
                .WithMany()
                .HasForeignKey(x => x.ResidentId)
                .IsRequired();
            modelBuilder.Entity<Contribution>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<Fee>()
                .HasOne<Apartment>()
                .WithMany()
                .HasForeignKey(x => x.ApartmentId)
                .IsRequired();
            modelBuilder.Entity<Fee>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<ServiceFee>()
                .HasOne<Fee>()
                .WithMany()
                .HasForeignKey(x => x.FeeId)
                .IsRequired();
            modelBuilder.Entity<ServiceFee>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<ServiceFee>()
                .HasOne<ServiceFeeType>()
                .WithMany()
                .HasForeignKey(x => x.TypeId)
                .IsRequired();
            modelBuilder.Entity<FeePayment>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<FeePayment>()
                .HasOne<Fee>()
                .WithMany()
                .HasForeignKey(x => x.FeeId)
                .IsRequired();
        }
    }
}
