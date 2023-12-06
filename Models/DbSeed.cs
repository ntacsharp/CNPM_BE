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
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CNPMDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CNPMDbContext>>()))
            {
            }
        }
    }
}
