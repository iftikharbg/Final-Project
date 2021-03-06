using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class UserCreateViewModel
    {
        
        public IFormFile Image { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }

        public DateTime JobStart { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

    }
}
