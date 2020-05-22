using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Data.Repository
{
    public interface IPatientRepository : IRepository
    {
        public IEnumerable<Patient> GetAllPatients();
        public Task<Patient> GetPatientByIdAsync(string userId);
        public Patient GetPatientByUserName(string userName);

        public void UpdatePatient(Patient patient);
    }
}
