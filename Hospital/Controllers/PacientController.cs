using Hospital.DAL;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{

    public class PacientController : Controller
    {

        private readonly AppDbContext _context;

        public PacientController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddPasient(PasientViewModel model)
        {


            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("", "Pasientin adini qeyd edin!");
                return View("Index");
            }
            if (string.IsNullOrEmpty(model.Surname))
            {
                ModelState.AddModelError("", "Pasientin soyadini qeyd edin!");
                return View("Index");
            }
           
            if (string.IsNullOrEmpty(model.PassportSerial))
            {
                ModelState.AddModelError("", "Pasientin passport nomresini qeyd edin!");
                return View("Index");
            }
            if (string.IsNullOrEmpty(model.Tin))
            {
                ModelState.AddModelError("", "Pasientin fin kodunu qeyd edin!");
                return View("Index");
            }
            if (string.IsNullOrEmpty(model.BloodGroup))
            {
                ModelState.AddModelError("", "Pasientin qan qrupunu qeyd edin!");
                return View("Index");
            }
            if (string.IsNullOrEmpty(model.Address))
            {
                ModelState.AddModelError("", "Pasientin adresini  qeyd edin!");
                return View("Index");
            }
            if (string.IsNullOrEmpty(model.Telephone))
            {
                ModelState.AddModelError("", "Pasientin telefon nomresini  qeyd edin!");
                return View("Index");
            }
            if (string.IsNullOrEmpty(model.Gender))
            {
                ModelState.AddModelError("", "Pasientin cinsini  qeyd edin!");
                return View("Index");
            }
            var pasient = new Pasient
            {
                Name = model.Name,
                Surname = model.Surname,
                Address = model.Address,
                BirthDate = model.BirthDate,
                BloodGroup = model.BloodGroup,
                Gender = model.Gender,
                PassportSerial = model.PassportSerial,
                Telephone = model.Telephone,
                Tin = model.Tin,
            };
            await _context.Pasients.AddAsync(pasient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> List()
        {
            var pasients = await _context.Pasients.Include(p=>p.Rezervations).Where(p => !p.IsDeleted).ToListAsync();
            return View(pasients);
        }


    }
}
