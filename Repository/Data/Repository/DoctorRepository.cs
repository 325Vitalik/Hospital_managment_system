using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public class DoctorRepository : IDoctorsRepository
    {
        private HospitalDbContext ctx;

        public DoctorRepository(HospitalDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void AddDoctor(Doctor doctor)
        {
            this.ctx.Doctors.Add(doctor);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return ctx.Doctors.Include(d => d.Patients).ToList();
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return ctx.Doctors.Include(d => d.Patients).ToList();
        }

        public async Task<Doctor> GetDoctorByIdAsync(string doctorId)
        {
            var doctor = await ctx.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
            if (doctor == null) return null;
            ctx.Entry(doctor).Collection(d => d.Patients).Load();
            return doctor;
        }

        public async Task<Doctor> GetDoctorByUserNameAsync(string userName)
        {
            var doctor = await this.ctx.Doctors.FirstOrDefaultAsync(d => d.UserName == userName);
            if (doctor == null) return null;
            ctx.Entry(doctor).Collection(d => d.Patients).Load();
            return doctor;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await ctx.SaveChangesAsync() > 0) return true;
            return false;
        }

        public void UpdateDoctor(Doctor doctor)
        {
            this.ctx.Doctors.Update(doctor);
        }
    }
}
