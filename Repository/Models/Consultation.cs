using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Models
{
    public class Consultation
    {
        public long Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }

        public ICollection<Drug> Drugs { get; set; }

        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
