using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class DoctorProcedures
    {
        public int Id { get; set; }

        public Procedure procedure { get; set; }

        public Doctor doctor { get; set; }
    }
}
