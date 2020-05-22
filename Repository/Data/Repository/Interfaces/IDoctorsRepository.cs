using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public interface IDoctorsRepository : IRepository
    {
        //GET Doctor
        public void AddDoctor(Doctor doctor);
        public Task<Doctor> GetDoctorByIdAsync(string doctorId);
        public Task<Doctor> GetDoctorByUserNameAsync(string userName);
        public IEnumerable<Doctor> GetAllDoctors();
        public Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        public void UpdateDoctor(Doctor doctor);
    }
}
