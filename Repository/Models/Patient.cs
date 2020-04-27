using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataBase.Models
{
    public class Patient : User
    {
        public string Diagnosis { get; set; }
        public int Chamber { get; set; }

        public string DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public ICollection<Consultation> Consultations { get; set; }
    }
}
