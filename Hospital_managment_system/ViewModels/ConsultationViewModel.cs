using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_managment_system.ViewModels
{
    public class ConsultationViewModel
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        public string PatientId { get; set; }
    }
}
