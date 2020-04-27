using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_managment_system.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private IRepository repo;

        public PatientsController(IRepository repo)
        {
            this.repo = repo;
        }

        //GET: api/Patients
        // Returns json with all patients data
        [HttpGet]
        public ActionResult<string> GetAllPatients()
        {
            var patients = repo.GetAllPatients();
            if (patients == null) return NotFound();

            patients.Select(p => { p.Doctor.Patients = null; p.Doctor.Consultations = null ; return p; }).ToList();
            return JsonSerializer.CreateJson(patients);
        }

        //GET: api/Patients/ByDoctorId/{doctorId}
        //Returns json with all putients of doctor with given id
        //When doctor not found returns 404
        [Route("ByDoctorId/{doctorId}")]
        [HttpGet]
        public ActionResult<string> GetPatientsOfDoctor(string doctorId)
        {
            var patients = repo.GetAllPatients();
            if (patients == null) return NotFound();

            patients = patients.Where(p => p.DoctorId == doctorId).Select(p => { p.Doctor.Patients = null; p.Doctor.Consultations = null; return p; }).ToList();
            return JsonSerializer.CreateJson(patients);
        }

        //GET: api/Patients/ById/{id}
        //Returns patient object in json format
        [Route("ById/{id}")]
        [HttpGet]
        public async Task<ActionResult<string>> GetPatientById(string id)
        {
            var patient =  await repo.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();

            patient.Doctor.Patients = null;
            patient.Doctor.Consultations = null;
            return JsonSerializer.CreateJson(patient);
        }

        //GET: api/Patients/ByUserName/{username}
        //Returns patient object in json
        [Route("ByUserName/{username}")]
        [HttpGet]
        public ActionResult<string> GetPatientByUsername(string userName)
        {
            var patient = repo.GetPatientByUserName(userName);
            if (patient == null) return NotFound();

            patient.Doctor.Patients = null;
            patient.Doctor.Consultations = null;
            return JsonSerializer.CreateJson(patient);
        }
    }
}