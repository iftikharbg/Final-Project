using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class PacientSendViewModel
    {
        public List<Procedure> Procedures { get; set; }

        public int DoctorId { get; set; }
        public DateTime StartDate { get; set; }
        public int Price { get; set; }
        public int ProcedureId { get; set; }
        public int UserId { get; set; }
    }
}
