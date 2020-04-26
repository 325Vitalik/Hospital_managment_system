using DataBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_managment_system.ViewModels.DoctorViewModels
{
    public class DoctorSignUpViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        [Required(ErrorMessage = "Please enter Username")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

        public ICollection<Patient> Patients { get; set; } = new HashSet<Patient>();
    }
}
