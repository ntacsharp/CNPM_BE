using CNPM_BE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM_BE.Models
{
    internal class DbSeed
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CNPMDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CNPMDbContext>>()))
            {
                if(context.VehicleType.Any())
                {
                    return; //DB has been seeded
                }
                var vt1 = new VehicleType();
                vt1.Name = "Xe ô tô";
                vt1.ParkingFee = 1200000;
                await context.VehicleType.AddAsync(vt1);
                var vt2 = new VehicleType();
                vt2.Name = "Xe máy";
                vt2.ParkingFee = 70000;
                await context.VehicleType.AddAsync(vt2);


                await context.SaveChangesAsync();
            }
        }
    }
}
