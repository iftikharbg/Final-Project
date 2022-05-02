using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PassportSerial { get; set; }
        public string PassportTin { get; set; }

        public DateTime BirthDate { get; set; }

        public string Blood { get; set; }

        public string Address { get; set; }
        public string Telephone { get; set; }

        public string Gender { get; set; }

        public float Salary { get; set; }

        public DateTime JobStart { get; set; } = DateTime.Now;

        public DateTime? JobEnd { get; set; }

        public string Image { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
