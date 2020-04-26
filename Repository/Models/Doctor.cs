using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Models
{
    public class Doctor : User
    {
        public string Department { get; set; }
        public string About { get; set; }

        public ICollection<Patient> Patients { get; set; }
        public ICollection<Consultation> Consultations { get; set; }
    }
}
