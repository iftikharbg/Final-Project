using Hospital.Areas.Admin.Utils;
using Hospital.DAL;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _user;
        public UserController(AppDbContext context,UserManager<User> user)
        {
            _user = user;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var staff = await _context.Users.Where(s => s.JobEnd == null&&!s.IsDeleted).ToListAsync();
            return View(staff);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.Users.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            user.IsDeleted = true;
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Update(string id)
        {
            var model = await _context.Users.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var viewModel = new UserUpdateViewModel
            {
               Id = model.Id,
               Name = model.Name,
               Surname = model.Surname,
               BirthDate = model.BirthDate,
               Gender = model.Gender,
               JobStart = model.JobStart,
               JobEnd = model.JobEnd,
               IsUserWorking = model.JobEnd == null,
               ImagePath = model.Image,
            };
               

            
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id,UserUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var user = await _context.Users.FindAsync(id);
            if (user==null)
            {
                return BadRequest();
            }
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;
            user.JobStart = model.JobStart;
            if (!model.IsUserWorking)
            {
                user.JobEnd = model.JobEnd;
            }

           
            if (model.Image !=null)
            {
                var fileName = FileUtils.CreateFile(FileConstants.ImagePath, model.Image);
                user.Image = fileName;
                
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }

        [HttpGet]

        public async Task<IActionResult> AddStaff()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddStaff(UserCreateViewModel model)
        {
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var user = new User
                {
                    JobStart = model.JobStart,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    UserName = model.UserName
                };

                if (model.Image != null)
                {
                    var fileName = FileUtils.CreateFile(FileConstants.ImagePath, model.Image);
                    user.Image = fileName;

                }
              var response =  await _user.CreateAsync(user, model.Password);
              
                await _user.AddToRoleAsync(user, RoleConstants.Reception);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        
        }

    }
}
