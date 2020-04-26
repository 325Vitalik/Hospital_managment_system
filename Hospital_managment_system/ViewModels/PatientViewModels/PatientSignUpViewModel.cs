using DataBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_managment_system.ViewModels.PatientViewModels
{
    public class PatientSignUpViewModel
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int Age { get; set; }
        public int Chamber { get; set; }
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "Please enter Username")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public ICollection<Consultation> Consultations { get; set; } = new HashSet<Consultation>();
    }
}
