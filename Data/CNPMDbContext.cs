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
        }
    }
}
