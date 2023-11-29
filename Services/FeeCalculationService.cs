using CNPM_BE.Data;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;
using NCrontab;
using System;

namespace CNPM_BE.Services
{
    public class FeeCalculationService : IHostedService
    {
        private readonly CNPMDbContext _context;
        private DateTime _nextRun = new DateTime();
        private const string Schedule = "0 0 1 * *";
        private readonly CrontabSchedule _crontabSchedule;
        private Timer _timer;

        public FeeCalculationService(CNPMDbContext context)
        {
            _context = context;
            _crontabSchedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
            _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            FeeCalculate(null);
            _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
            var delay = _nextRun - DateTime.Now;
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var interval = TimeSpan.FromDays(daysInMonth);

            _timer = new Timer(FeeCalculate, null, delay, interval);

            return Task.CompletedTask;
        }
        public async void FeeCalculate(object state)
        {
            var current = DateTime.Now;
            int m = current.Month, y = current.Year;
            if (m == 1)
            {
                m = 12;
                y--;
            }
            else
            {
                m--;
            }
            var chfList = await _context.CurrentHouseholdFee.Where(c => c.IsActive).ToListAsync();
            foreach(var chf in chfList)
            {
                var hfh = new HouseholdFeeHistory();
                hfh.Month = m;
                hfh.Year = y;
                hfh.CurrentManagementFee = chf.CurrentManagementFee;
                hfh.CurrentParkingFee = chf.CurrentParkingFee;
                hfh.CurrentServiceFee = chf.CurrentServiceFee;
                hfh.LeftoverManagementFee = chf.LeftoverManagementFee;
                hfh.LeftoverParkingFee = chf.LeftoverParkingFee;
                hfh.LeftoverServiceFee = chf.LeftoverServiceFee;
                hfh.TotalManagementFee = chf.TotalManagementFee;
                hfh.TotalParkingFee = chf.TotalParkingFee;
                hfh.TotalServiceFee = chf.TotalServiceFee;
                hfh.PaidManagementFee = chf.PaidManagementFee;
                hfh.PaidParkingFee = chf.PaidParkingFee;
                hfh.PaidServiceFee = chf.PaidServiceFee;

                chf.LeftoverManagementFee = chf.TotalManagementFee - chf.PaidManagementFee;
                chf.LeftoverParkingFee = chf.TotalParkingFee - chf.PaidParkingFee;
                chf.LeftoverServiceFee = chf.TotalServiceFee - chf.PaidServiceFee;
                chf.TotalManagementFee = chf.LeftoverManagementFee + chf.CurrentManagementFee;
                chf.TotalParkingFee = chf.LeftoverParkingFee + chf.CurrentParkingFee;
                chf.CurrentServiceFee = chf.LeftoverServiceFee + chf.CurrentServiceFee;
                chf.PaidManagementFee = chf.PaidParkingFee = chf.PaidServiceFee = 0;
                try
                {
                    await _context.HouseholdFeeHistory.AddAsync(hfh);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return;
                }
            }

        }
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
