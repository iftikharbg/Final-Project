using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class AddDoctorViewModel
    {
        public List<Doctor> doctors { get; set; }
        public List<Doctor> ProcedureDoctors { get; set; }
        public int Id { get; set; }

        public AddDoctorProcedureViewModel AddDoctorProcedure { get; set; }
        public Procedure procedure { get; set; }
    }
}
