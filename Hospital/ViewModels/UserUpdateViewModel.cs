using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class UserUpdateViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public IFormFile Image { get; set; }

        public string Gender { get; set; }

        public DateTime JobStart { get; set; }

        public DateTime? JobEnd { get; set; }

        public bool IsUserWorking { get; set; }

        public string ImagePath { get; set; }
    }
}
