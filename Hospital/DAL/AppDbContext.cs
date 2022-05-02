using Hospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Pasient> Pasients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<DoctorProcedures> DoctorProcedures { get; set; }

        public DbSet<Rezervations> Rezervations { get; set; }

    }
}
