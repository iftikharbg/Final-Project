using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class PasientUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public string PassportSerial { get; set; }

        public string Tin { get; set; }

        public string BloodGroup { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public string Gender { get; set; }
    }
}
