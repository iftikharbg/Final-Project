using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Rezervations
    {
        public int Id { get; set; }
        

        public Procedure Procedure { get; set; }

        public Pasient Pasient { get; set; }
        public int Price { get; set; }

        public Doctor Doctor { get; set; }

        public DateTime StartDate { get; set; }

    }
}
