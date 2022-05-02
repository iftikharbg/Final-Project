using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class ProcedureAddViewModel
    {
        public string Name { get; set; }

        public List<Doctor> doctors { get; set; }
        public int ProcessTime { get; set; }
    }
}
