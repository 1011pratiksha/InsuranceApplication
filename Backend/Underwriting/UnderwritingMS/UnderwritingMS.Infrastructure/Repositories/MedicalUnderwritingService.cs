
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Domain.Entities;
using UnderwritingMS.Infrastructure.Data;

namespace UnderwritingMS.Infrastructure.Services
{
    public class MedicalUnderwritingService : IMedicalUnderwritingService
    {
        AppDbContext db;
        IMapper mapper;

        public MedicalUnderwritingService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddMedicalDetails(
            int id, medical_underwritingDTO dto)
        {
            var underwriting = await db.underwriting_case.FindAsync(id);

            if (underwriting == null)
                throw new Exception("Underwriting case not found.");

            var existing = await db.medical_underwriting
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (existing != null)
                throw new Exception("Medical details already exist.");

            if (dto.height <= 0 || dto.weight <= 0)
                throw new Exception("Height and weight must be greater than zero.");

            var d = mapper.Map<medical_underwriting>(dto);

            d.medical_id = 0;
            d.underwriting_id = id;
            d.bmi = Math.Round(dto.weight / ((dto.height / 100m) * (dto.height / 100m)), 2);

            await db.medical_underwriting.AddAsync(d);

            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = id,
                    action = "Medical Details Added",
                    remarks = "Medical assessment details recorded.",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();
        }

        public async Task<medical_underwritingDTO?> GetMedicalDetails(int id)
        {
            var d = await db.medical_underwriting
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (d == null)
                return null;

            return mapper.Map<medical_underwritingDTO>(d);
        }

        public async Task UpdateMedicalDetails(
            int id, medical_underwritingDTO dto)
        {
            var d = await db.medical_underwriting
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (d == null)
                throw new Exception("Medical details not found.");

            if (dto.height <= 0 || dto.weight <= 0)
                throw new Exception("Height and weight must be greater than zero.");

            d.height = dto.height;
            d.weight = dto.weight;
            d.bmi = Math.Round(dto.weight / ((dto.height / 100m) * (dto.height / 100m)), 2);
            d.blood_pressure = dto.blood_pressure;
            d.diabetes = dto.diabetes;
            d.heart_disease = dto.heart_disease;
            d.cancer_history = dto.cancer_history;
            d.smoking = dto.smoking;
            d.alcohol = dto.alcohol;
            d.medical_history = dto.medical_history;
            d.medical_report_path = dto.medical_report_path;

            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = id,
                    action = "Medical Details Updated",
                    remarks = "Medical assessment details updated.",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();
        }
    }
}
