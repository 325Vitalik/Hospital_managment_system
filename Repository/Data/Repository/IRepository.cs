using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public interface IRepository
    {
        public Task<bool> SaveChangesAsync();

        public void AddDoctor(Doctor doctor);
        public Task<Doctor> GetDoctorByIdAsync(string doctorId);
        public Task<Doctor> GetDoctorByUserNameAsync(string userName);
        public IEnumerable<Doctor> GetAllDoctors();
        public Task<IEnumerable<Doctor>> GetAllDoctorsAsync();


        public Task<Patient> GetPatientByIdAsync(string userId);
        public Patient GetPatientByUserName(string userName);

        public Task<User> GetUserByUserNameAsync(string userName);

        public void UpdatePatient(Patient patient);

        public void UpdateDoctor(Doctor doctor);

        public void addPost(Post post);
        public IEnumerable<Post> GetAllPosts();
        public Task<Post> GetPostByIdAsync(long id);
    }
}
