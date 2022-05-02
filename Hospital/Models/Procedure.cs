using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Procedure
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProcessTime { get; set; }

        public List<DoctorProcedures> DoctorProcedures { get; set; }
        public bool IsDeleted { get; set; }
    }
}
