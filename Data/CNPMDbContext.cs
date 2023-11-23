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
        public DbSet<Household> Household { get; set; }
        public DbSet<HouseholdMember> HouseholdMember { get; set; }
        public DbSet<HouseholdDonation> HouseholdDonation { get; set; }
        public DbSet<DonationFund> DonationFund { get; set; }
        public DbSet<Donation> Donation { get; set; }
        public DbSet<HouseholdFee> HouseholdFee { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSnakeCaseNamingConvention();
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            modelBuilder.Entity<Household>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.ManagerId)
                .IsRequired();
            modelBuilder.Entity<HouseholdMember>()
                .HasOne<Household>()
                .WithMany()
                .HasForeignKey(x => x.HouseholdId)
                .IsRequired();
            modelBuilder.Entity<Donation>()
                .HasOne<DonationFund>()
                .WithMany()
                .HasForeignKey(x => x.FundId)
                .IsRequired();
            modelBuilder.Entity<Donation>()
                .HasOne<Household>()
                .WithMany()
                .HasForeignKey(x => x.DonatorId)
                .IsRequired();
            modelBuilder.Entity<DonationFund>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .IsRequired();
            modelBuilder.Entity<HouseholdFee>()
                .HasOne<Household>()
                .WithMany()
                .HasForeignKey(x => x.HouseholdId)
                .IsRequired();
            modelBuilder.Entity<HouseholdDonation>()
                .HasOne<Household>()
                .WithMany()
                .HasForeignKey(x => x.DonatorId)
                .IsRequired();
            modelBuilder.Entity<HouseholdDonation>()
                .HasOne<DonationFund>()
                .WithMany()
                .HasForeignKey(x => x.FundId)
                .IsRequired();
        }
    }
}
