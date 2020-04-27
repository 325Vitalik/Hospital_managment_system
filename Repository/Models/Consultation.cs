using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Doctor Doctor { get; set; }

        public string PatientId { get; set; }
        [JsonIgnore]
        public Patient Patient { get; set; }
    }
}
