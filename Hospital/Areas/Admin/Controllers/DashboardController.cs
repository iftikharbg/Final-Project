using Hospital.DAL;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors.Where(d=>!d.IsDeleted).Include(d=>d.DoctorRezervations).OrderBy(d => d.Name).Take(4).ToListAsync();
            var model = new DashBoardViewModel {
                Doctors = doctors
        };
            return View(model);
        }
    }
}
