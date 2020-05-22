using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private HospitalDbContext ctx;

        public PatientRepository(HospitalDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await ctx.SaveChangesAsync() > 0) return true;
            return false;
        }

        public void UpdatePatient(Patient patient)
        {
            this.ctx.Patients.Update(patient);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return ctx.Patients.Include(p => p.Doctor).Include(p => p.Consultations).ToList();
        }

        public async Task<Patient> GetPatientByIdAsync(string userId)
        {
            var patient = await this.ctx.Patients.FirstOrDefaultAsync(p => p.Id == userId);
            if (patient == null) return null;
            ctx.Entry(patient).Reference(p => p.Doctor).Load();
            ctx.Entry(patient).Collection(p => p.Consultations).Load();
            return patient;
        }

        public Patient GetPatientByUserName(string userName)
        {
            var patient = this.ctx.Patients.FirstOrDefault(p => p.UserName == userName);
            ctx.Entry(patient).Reference(p => p.Doctor).Load();
            ctx.Entry(patient).Collection(p => p.Consultations).Load();
            return patient;
        }
    }
}
