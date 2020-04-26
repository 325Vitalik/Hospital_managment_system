using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public class Repository : IRepository
    {
        private HospitalDbContext ctx;

        public Repository(HospitalDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void AddDoctor(Doctor doctor)
        {
            this.ctx.Doctors.Add(doctor);
        }

        public async void addPost(Post post)
        {
            await this.ctx.Posts.AddAsync(post);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return ctx.Doctors.Include(d => d.Patients).ToList();
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return ctx.Doctors.Include(d => d.Patients).ToList();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return ctx.Posts.ToList();
        }

        public async Task<Doctor> GetDoctorByIdAsync(string doctorId)
        {
            var doctor = await ctx.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
            ctx.Entry(doctor).Collection(d => d.Patients).Load();
            return doctor;
        }

        public async Task<Doctor> GetDoctorByUserNameAsync(string userName)
        {
            var doctor = await this.ctx.Doctors.FirstOrDefaultAsync(d => d.UserName == userName);
            ctx.Entry(doctor).Collection(d => d.Patients).Load();
            return doctor;
        }

        public async Task<Patient> GetPatientByIdAsync(string userId)
        {
            var patient = await this.ctx.Patients.FirstOrDefaultAsync(p => p.Id == userId);
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

        public async Task<Post> GetPostByIdAsync(long id)
        {
            return await ctx.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await this.ctx.Users.FirstOrDefaultAsync(u => u.UserName == userName);
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

        public void UpdatePatient(Patient patient)
        {
            this.ctx.Patients.Update(patient);
        }
    }
}
