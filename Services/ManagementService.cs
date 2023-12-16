﻿using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace CNPM_BE.Services
{
    public class ManagementService
    {
        private readonly CNPMDbContext _context;
        private readonly TimeConverterService _timeConverterService;
        public ManagementService(CNPMDbContext context, TimeConverterService timeConverterService)
        {
            _context = context;
            _timeConverterService = timeConverterService;
        }

        public async Task<ApiResponseExpose<Resident>> AddResident(AppUser user, Resident req)
        {
            var resp = new ApiResponseExpose<Resident>();

            var newResident = new Resident();
            var ex = await _context.Resident.FirstOrDefaultAsync(r => r.ResidentCode == req.ResidentCode && r.CreatorId == user.Id && r.Status == ResidentStatus.Active);
            if (ex != null)
            {
                resp.code = -1;
                resp.message = "Đã tồn tại cư dân với mã " + req.ResidentCode;
                return resp;
            }

            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == req.ApartmentId && a.Status != ApartmentStatus.Deleted && a.CreatorId == user.Id);
            if (apartment == null)
            {
                resp.code = -1;
                resp.message = "Căn hộ không hợp lệ";
                return resp;
            }

            newResident.CreatorId = user.Id;
            newResident.ResidentCode = req.ResidentCode;
            newResident.ApartmentId = req.ApartmentId;
            newResident.Name = req.Name;
            newResident.BirthDate = await _timeConverterService.ConvertToUTCTime(req.BirthDate);
            newResident.Career = req.Career;
            newResident.Gender = (ResidentGender)req.Gender;
            newResident.Status = ResidentStatus.Active;
            newResident.CreatedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);

            try
            {
                await _context.Resident.AddAsync(newResident);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm cư dân mã " + req.ResidentCode;
                return resp;
            }
            newResident = await _context.Resident.OrderBy(r => r.Id).LastOrDefaultAsync(r => r.CreatorId == user.Id && r.Status == ResidentStatus.Active);
            if (apartment.Status == ApartmentStatus.Unoccupied)
            {
                apartment.Status = ApartmentStatus.Occupied;
                apartment.OwnerId = newResident.Id;
                var con = new Contribution();
                con.ApartmentId = apartment.Id;
                con.CreatorId = user.Id;
                con.Status = ContributionStatus.Active;
                con.ForThePoor = con.ForVNSeasAndIslands = con.DGFestival = con.ResidentialGroup = con.ForChildren = con.Charity = con.Gratitude = con.StudyPromotion = con.ForTheElderly = 0;
                try
                {
                    await _context.AddAsync(con);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    resp.code = -1;
                    resp.message = "Đã có lỗi xảy ra trong quá trình thêm cư dân mã " + req.ResidentCode;
                    return resp;
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm cư dân mã " + req.ResidentCode;
                return resp;
            }

            resp.code = 1;
            resp.message = "Thêm cư dân mã " + req.ResidentCode + " thành công";
            resp.entity = newResident;

            return resp;
        }

        public async Task<ApiResponseExpose<Resident>> RemoveResident(AppUser user, int req)
        {
            var resp = new ApiResponseExpose<Resident>();
            var resident = await _context.Resident.FirstOrDefaultAsync(r => r.CreatorId == user.Id && r.Id == req && r.Status == ResidentStatus.Active);
            if (resident == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm cư dân";
                return resp;
            }
            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == resident.ApartmentId);
            var count = await _context.Resident.Where(r => r.ApartmentId == resident.ApartmentId && r.Status != ResidentStatus.Deleted).CountAsync();
            if (count <= 1)
            {
                apartment.Status = ApartmentStatus.Unoccupied;
                apartment.OwnerId = null;
                var contribution = await _context.Contribution.FirstOrDefaultAsync(c => c.ApartmentId == apartment.Id && c.Status == ContributionStatus.Active);
                contribution.Status = ContributionStatus.Deleted;
            }
            else
            {
                var newOwner = await _context.Resident.FirstOrDefaultAsync(r => r.ApartmentId == apartment.Id && r.CreatorId == user.Id && r.Status == ResidentStatus.Active);
                apartment.OwnerId = newOwner.Id;
            }
            resident.Status = ResidentStatus.Deleted;
            resident.DeletedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
            var vehicleList = await _context.Vehicle.Where(v => v.OwnerId == resident.Id && v.CreatorId == user.Id && v.Status == VehicleStatus.Active).ToListAsync();
            foreach (var vehicle in vehicleList)
            {
                vehicle.Status = VehicleStatus.Deleted;
                vehicle.DeletedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình xóa cư dân";
                return resp;
            }

            resp.code = 1;
            resp.message = "Xóa cư dân thành công";
            resp.entity = resident;

            return resp;
        }

        public async Task<ApiResponseExpose<Resident>> UpdateInformation(AppUser user, Resident req)
        {
            var resp = new ApiResponseExpose<Resident>();
            var resident = await _context.Resident.FirstOrDefaultAsync(r => r.Id ==  req.Id && r.CreatorId == user.Id && r.Status == ResidentStatus.Active);
            if (resident == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm cư dân";
                return resp;
            }
            resident.Name = req.Name;
            resident.Career = req.Career;
            resident.Gender = (ResidentGender)req.Gender;
            resident.BirthDate = await _timeConverterService.ConvertToUTCTime(req.BirthDate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin cư dân";
                return resp;
            }

            resp.code = 1;
            resp.message = "Cập nhật thông tin cư dân thành công";
            resp.entity = resident;

            return resp;
        }

        public async Task<List<ResidentResp>> GetResidentList(AppUser user)
        {
            var list = await _context.Resident.Where(r => r.CreatorId == user.Id && r.Status == ResidentStatus.Active).ToListAsync();
            var resp = new List<ResidentResp>();
            foreach(var r in list)
            {
                var a = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == r.ApartmentId);
                var rr = new ResidentResp(r, a);
                resp.Add(rr);
            }
            return resp;
        }

        public async Task<List<HouseholdResp>> GetHouseholdList(AppUser user)
        {
            var apartmentList = await _context.Apartment.Where(a => a.CreatorId == user.Id && a.Status == ApartmentStatus.Occupied).ToListAsync();
            var resp = new List<HouseholdResp>();
            foreach (var a in apartmentList)
            {
                var hr = new HouseholdResp();
                hr.Id = a.Id;
                hr.Position = a.Position;
                hr.ApartmentCode = a.ApartmentCode;
                var owner = await _context.Resident.FirstOrDefaultAsync(r => r.Id == a.OwnerId);
                hr.OwnerCode = owner.ResidentCode;
                hr.OwnerName = owner.Name;
                var list = new List<ResidentResp>();
                var residentList = await _context.Resident.Where(r => r.ApartmentId == a.Id && r.Status == ResidentStatus.Active).ToListAsync();
                foreach(var res in residentList)
                {
                    var rr = new ResidentResp(res, a);
                    list.Add(rr);
                }
                hr.ResidentList = list;
                resp.Add(hr);
            }
            return resp;
        }
        public async Task<List<HouseholdOption>> GetOptionList(AppUser user)
        {
            var apartmentList = await _context.Apartment.Where(a => a.CreatorId == user.Id && a.Status == ApartmentStatus.Occupied).ToListAsync();
            var resp = new List<HouseholdOption>();
            foreach(var apartment in apartmentList)
            {
                var owner = await _context.Resident.FirstOrDefaultAsync(r => r.ApartmentId == apartment.Id && r.Status == ResidentStatus.Active);
                var ho = new HouseholdOption(apartment, owner);
                resp.Add(ho);
            }
            return resp;
        }
    }
}
