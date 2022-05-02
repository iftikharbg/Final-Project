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
    public class ProcedureController : Controller
    {
        private readonly AppDbContext _context;

        public ProcedureController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var procedures = await _context.Procedures.Where(p =>!p.IsDeleted).ToListAsync();
            return View(procedures);

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var doctor = await _context.Doctors.Where(d => !d.IsDeleted).ToListAsync();
            var model = new ProcedureAddViewModel
            {
                doctors = doctor,

            };
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Add(ProcedureAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var procedure = new Procedure
            {
                ProcessTime = model.ProcessTime,
                Name = model.Name,
                IsDeleted = false,
            };
           await _context.Procedures.AddAsync(procedure);
           await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var procedure = await _context.Procedures.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (procedure == null)
            {
                return NotFound();
            }
            procedure.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _context.Procedures.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var viewModel = new ProcedureUpdateViewModel
            {
                Id = model.Id,
                Name = model.Name,
                ProcessTime = model.ProcessTime,
            };



            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProcedureUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null)
            {
                return BadRequest();
            }
            procedure.Name = model.Name;
            procedure.ProcessTime = model.ProcessTime;
           


            

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> AddDoctor(int id)
        {
            var doctors = await _context.Doctors.Where(d => !d.IsDeleted).ToListAsync();
            var procedure = await _context.Procedures.Where(p => !p.IsDeleted&&p.Id==id).FirstOrDefaultAsync();
            var procedureDoctors = await _context.DoctorProcedures.Include(d => d.doctor).Include(p => p.procedure).Where(p => p.procedure.Id == id).Select(s=>s.doctor).ToListAsync();
            var model = new AddDoctorViewModel

            {
                Id = id,
                doctors = doctors,
                procedure = procedure,
                ProcedureDoctors = procedureDoctors
            };

            return View(model);
        }

        [HttpPost]
       
        public async Task<IActionResult> AddDoctor(AddDoctorViewModel model)
        {
            var doctors = await _context.Doctors.Where(d => !d.IsDeleted&&d.Id==model.AddDoctorProcedure.DoctorId).FirstOrDefaultAsync();
            if (doctors==null)
            {
                return NotFound();
            }
            
            var procedure = await _context.Procedures.Where(p => !p.IsDeleted && p.Id == model.AddDoctorProcedure.ProcedureId).FirstOrDefaultAsync();
            if (procedure == null)
            {
                return NotFound();
            }
            var proceduredoctor = await _context.DoctorProcedures.Include(p=>p.doctor).Include(p=>p.procedure).Where(p => p.doctor.Id == model.AddDoctorProcedure.DoctorId&&p.procedure.Id==model.AddDoctorProcedure.ProcedureId).FirstOrDefaultAsync();
            if (proceduredoctor==null)
            {
                var _model = new DoctorProcedures
                {
                    doctor = doctors,
                    procedure = procedure,

                };
               await _context.DoctorProcedures.AddAsync(_model);

            }
            else
            {
                _context.DoctorProcedures.Remove(proceduredoctor);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> DeleteDoctor(AddDoctorViewModel model)
        {
            var proceduredoctor = await _context.DoctorProcedures.Include(p => p.doctor).Include(d=>d.procedure).Where(p => p.doctor.Id == model.AddDoctorProcedure.DoctorId&&p.procedure.Id==model.AddDoctorProcedure.ProcedureId).ToListAsync();
            foreach (var item in proceduredoctor)
            {
                _context.DoctorProcedures.Remove(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> History()
        {
            var rezervation = await _context.Rezervations.Include(r => r.Pasient).Include(r => r.Procedure).ThenInclude(r => r.DoctorProcedures).ThenInclude(r => r.doctor).Include(r=>r.Doctor).ToListAsync();
            var model = new HistoryViewModel
            {
                Rezervations=rezervation,
            };
            return View(model);
        }
    }
}
