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
                //if(context.VehicleType.Any())
                //{
                //    return; //DB has been seeded
                //}
                //var vt1 = new VehicleType();
                //vt1.Name = "Ô tô";
                //vt1.ParkingFee = 1200000;
                //vt1.Status = VehicleTypeStatus.Active;
                //vt1.VehicleTypeCode = "VT001";
                //await context.VehicleType.AddAsync(vt1);
                //var vt2 = new VehicleType();
                //vt2.Name = "Xe máy";
                //vt2.ParkingFee = 70000;
                //vt2.Status = VehicleTypeStatus.Active;
                //vt2.VehicleTypeCode = "VT002";
                //await context.VehicleType.AddAsync(vt2);
                //var t1 = new ServiceFeeType();
                //t1.ServiceFeeTypeCode = "ST001";
                //t1.Name = "Tiền điện";
                //t1.PricePerUnit = 0;
                //t1.MeasuringUnit = MeasuringUnit.Number;
                //t1.Status = ServiceFeeTypeStatus.Active;
                //t1.IsSystem = true;
                //var t2 = new ServiceFeeType();
                //t2.ServiceFeeTypeCode = "ST002";
                //t2.Name = "Tiền nước";
                //t2.PricePerUnit = 0;
                //t2.MeasuringUnit = MeasuringUnit.M3;
                //t2.Status = ServiceFeeTypeStatus.Active;
                //t2.IsSystem = true;
                //var t3 = new ServiceFeeType();
                //t3.ServiceFeeTypeCode = "ST003";
                //t3.Name = "Phí dịch vụ chung cư";
                //t3.PricePerUnit = 0;
                //t3.MeasuringUnit = MeasuringUnit.M2;
                //t3.Status = ServiceFeeTypeStatus.Active;
                //t3.IsSystem = true;
                //var t4 = new ServiceFeeType();
                //t4.ServiceFeeTypeCode = "ST004";
                //t4.Name = "Phí quản lý chung cư";
                //t4.PricePerUnit = 7000;
                //t4.MeasuringUnit = MeasuringUnit.M2;
                //t4.Status = ServiceFeeTypeStatus.Active;
                //await context.ServiceFeeType.AddAsync(t1);
                //await context.ServiceFeeType.AddAsync(t2);
                //await context.ServiceFeeType.AddAsync(t3);
                //await context.ServiceFeeType.AddAsync(t4);
                //for (int i = 1; i <= 10; i++)
                //{
                //var kiot = new Apartment();
                //kiot.ApartmentCode = "K" + i.ToString("D3");
                //kiot.CreatorId = 1;
                //kiot.Position = "Kiot số " + i.ToString();
                //kiot.Area = 40;
                //kiot.RoomCount = 1;
                //kiot.Price = 6000000;
                //kiot.Status = ApartmentStatus.Unoccupied;
                //await context.AddAsync(kiot);
                //}
                //int cnt = 10;
                //for (int i = 6; i <= 29; i++)
                //{
                    
                //}
                await context.SaveChangesAsync();
            }
        }
    }
}
