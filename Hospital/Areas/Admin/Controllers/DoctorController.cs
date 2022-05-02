using Hospital.Areas.Admin.Utils;
using Hospital.DAL;
using Hospital.Models;
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
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;

        public DoctorController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors.Where(d=>!d.IsDeleted).ToListAsync();
            return View(doctors);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(DoctorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var doctor = new Doctor
            {
                Name = model.Name,
                Surname = model.Surname,
                BirtDate = model.BirtDate,
                
            };

            if (model.Photo != null)
            {
                var fileName = FileUtils.CreateFile(FileConstants.ImagePath, model.Photo);
                doctor.Photo = fileName;

            }
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
           
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.id = id;
            var doctor = await _context.Doctors.Where(d => !d.IsDeleted && d.Id == id).FirstOrDefaultAsync();
            var model = new DoctorUpdateViewModel
            {
                Name = doctor.Name,
                Surname = doctor.Surname,
                BirtDate = doctor.BirtDate,

                PhotoName = doctor.Photo,
            };
            return View(model);
            
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,DoctorUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor!=null)
            {
                doctor.Name = model.Name;
                doctor.Surname = model.Surname;

                doctor.BirtDate = model.BirtDate;

                if (model.Photo != null)
                {
                    var fileName = FileUtils.CreateFile(FileConstants.ImagePath, model.Photo);
                    doctor.Photo = fileName;

                }
            }
         
            else
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");




        }

        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (doctor == null)
            {
                return NotFound();
            }
            doctor.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
