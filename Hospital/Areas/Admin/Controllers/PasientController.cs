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
    public class PasientController : Controller
    {
        private readonly AppDbContext _context;

        public PasientController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var pasients = await _context.Pasients.Where(p=>!p.IsDeleted).ToListAsync();
            return View(pasients);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pasient = await _context.Pasients.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (pasient == null)
            {
                return NotFound();
            }
            pasient.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Pasient");
        }


        public async Task<IActionResult> Update(int id)
        {
            var model = await _context.Pasients.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var viewModel = new PasientUpdateViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                Address = model.Address,
                BloodGroup = model.BloodGroup,
                PassportSerial = model.PassportSerial,
                Telephone = model.Telephone,
                Tin = model.Tin,
                
            };



            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, PasientUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var pasient = await _context.Pasients.FindAsync(id);
            if (pasient == null)
            {
                return BadRequest();
            }

            pasient.Name = model.Name;
            pasient.Surname = model.Surname;
            pasient.BirthDate = model.BirthDate;
            pasient.Gender = model.Gender;
            pasient.Address = model.Address;
            pasient.BloodGroup = model.BloodGroup;
            pasient.PassportSerial = model.PassportSerial;
            pasient.Telephone = model.Telephone;
            pasient.Tin = model.Tin;
          


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> AddDoctor()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Send(int id)
        {
            var procedure = await _context.Procedures.Include(p => p.DoctorProcedures).ThenInclude(s=>s.doctor).Where(d =>!d.IsDeleted).ToListAsync();
            var model = new PacientSendViewModel
            {
                UserId= id,
                Procedures = procedure,
            };
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Send(int id, PacientSendViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (model.Price == 0)
            {
                ModelState.AddModelError("", "Qiymet 0 ola bilmez!");
                var p = await _context.Procedures.Include(p => p.DoctorProcedures).ThenInclude(s => s.doctor).Where(d => !d.IsDeleted).ToListAsync();
                var m = new PacientSendViewModel
                {
                    UserId = id,
                    Procedures = p,
                };
                return View("Send",m);

            }
            if (model.StartDate<DateTime.Now)
            {
                ModelState.AddModelError("", "Tarix kecmish zamanda ola bilmez!");
                var p = await _context.Procedures.Include(p => p.DoctorProcedures).ThenInclude(s => s.doctor).Where(d => !d.IsDeleted).ToListAsync();
                var m = new PacientSendViewModel
                {
                    UserId = id,
                    Procedures = p,
                };
                return View("Send", m);
            }
            var pacient = await _context.Pasients.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            var procedure = await _context.Procedures.Where(p => !p.IsDeleted && p.Id == model.ProcedureId).FirstOrDefaultAsync();
            var doctor = await _context.Doctors.Where(d => !d.IsDeleted && d.Id == model.DoctorId).FirstOrDefaultAsync();
            var _model = new Rezervations
            {
                Procedure = procedure,
                Pasient = pacient,
                Price = model.Price,
                StartDate = model.StartDate,
                Doctor = doctor,
            };
            await _context.Rezervations.AddAsync(_model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home", new { area = "" });


        }
    }
}
