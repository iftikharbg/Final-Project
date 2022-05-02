using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime BirtDate { get; set; }

        public string Photo { get; set; }

        public List<Rezervations> DoctorRezervations { get; set; }
        public List<DoctorProcedures> procedures { get; set; }

        public bool IsDeleted { get; set; }
    }
}
