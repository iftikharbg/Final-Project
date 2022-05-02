using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class DoctorUpdateViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string PhotoName { get; set; }
        public DateTime BirtDate { get; set; }

        public IFormFile Photo { get; set; }
    }
}
