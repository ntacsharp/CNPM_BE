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
        public DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSnakeCaseNamingConvention();
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
