﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Data.Repository;
using DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Hospital_managment_system.ViewModels;
using Hospital_managment_system.ViewModels.DoctorViewModels;

namespace Hospital_managment_system.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IRepository repo;
        private UserManager<User> userMgr;

        public ProfileController(IRepository repo, UserManager<User> userMgr)
        {
            this.repo = repo;
            this.userMgr = userMgr;
        }

        //This action returns profile of current user
        public async Task<IActionResult> Profile()
        {
            var user = await repo.GetUserByUserNameAsync(User.Identity.Name);
            var role = (await userMgr.GetRolesAsync(user))?.FirstOrDefault();
            if(role == "Doctor")
            {
                var doctor = repo.GetDoctorByUserNameAsync(User.Identity.Name);

                return View("DoctorProfile", new Tuple<Doctor, DoctorEditViewModel>(await doctor, new DoctorEditViewModel()));
            }
            else if(role == "Patient")
            {
                var patient = repo.GetPatientByUserName(User.Identity.Name);
                patient.Consultations = patient.Consultations.OrderByDescending(x => x.Time).ToList();
                return View("PatientProfile", new Tuple<Patient, ConsultationViewModel>(patient, new ConsultationViewModel()));
            }
            else if(role == "Admin")
            {
                return View("AdminProfile", new Tuple<User, IEnumerable<Doctor>>(user, await repo.GetAllDoctorsAsync()));
            }
            else
            {
                return View("Error");
            }
        }

        //get profile of doctor from admin or patient role
        [HttpGet]
        public async Task<IActionResult> DoctorProfile(string id)
        {
            var doctor = await repo.GetDoctorByIdAsync(id);
            if (User.IsInRole("Patient"))
            {
                var patient = repo.GetPatientByUserName(User.Identity.Name);
                if (patient.DoctorId != doctor.Id)
                {
                    return new RedirectResult("/Denied");
                }
            }
            return View("DoctorProfile", new Tuple<Doctor, DoctorEditViewModel>(doctor, new DoctorEditViewModel()));
        }

        //get profile of patient from doctor or admin role
        [HttpGet]
        public async Task<IActionResult> PatientProfile(string id)
        {
            var patient = await repo.GetPatientByIdAsync(id);
            patient.Consultations = patient.Consultations.OrderByDescending(x => x.Time).ToList();

            if (User.IsInRole("Doctor"))
            {
                var doctor = await repo.GetDoctorByUserNameAsync(User.Identity.Name);
                if (!doctor.Patients.Contains(patient))
                {
                    return new RedirectResult("/Denied");
                }
            }
            return View("PatientProfile", new Tuple<Patient, ConsultationViewModel>(patient, new ConsultationViewModel()));
        }

        //doctor can add consultation data in history of patient
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<IActionResult> PatientNewConsultation([Bind(Prefix = "Item2")]ConsultationViewModel vm)
        {
            var doctor = await repo.GetDoctorByUserNameAsync(User.Identity.Name);
            var patient = await repo.GetPatientByIdAsync(vm.PatientId);
            if (!doctor.Patients.Contains(patient))
            {
                return new RedirectResult("/Denied");
            }

            var consultation = new Consultation()
            {
                Header = vm.Header,
                Description = vm.Description,
                Time = vm.Time,
                DoctorId = doctor.Id,
                Doctor = doctor,
                PatientId = vm.PatientId,
                Patient = patient,
                Drugs = new HashSet<Drug>()
            };

            patient.Consultations = patient.Consultations ?? new HashSet<Consultation>();

            patient.Consultations.Add(consultation);

            patient.Consultations = patient.Consultations.OrderByDescending(x => x.Time).ToList();

            repo.UpdatePatient(patient);
            await repo.SaveChangesAsync();

            return PartialView("PatientHistory", patient);
        }

        //doctor or admin can change information in property "About" of doctor
        [Authorize(Roles = "Doctor,Admin")]
        [HttpPost]
        public async Task<IActionResult> DoctorEdit([Bind(Prefix = "Item2")]DoctorEditViewModel vm)
        {
            var doctor = await repo.GetDoctorByIdAsync(vm.Id);
            doctor.About = vm.About;

            repo.UpdateDoctor(doctor);
            await repo.SaveChangesAsync();

            return new RedirectResult(vm.CurrentLink);
        }
    }
}