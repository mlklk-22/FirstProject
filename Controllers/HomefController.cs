using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Diagnostics;

namespace FirstProject.Controllers
{
    public class HomefController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviroment;
        public HomefController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            this.webHostEnviroment = webHostEnviroment;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            var modelContext = _context.Homes.Include(h => h.Role).Include(h => h.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }
        public IActionResult Dashboard()
        {
            int countBook = 0;
            int countNotBook = 0;
            int NumberOfUsers = 0;
            int NumberOfTestmonial = 0;

            var bAndNReserve = _context.Reservations;
            foreach (var item in bAndNReserve)
            {
                if (item.Status == "Full")
                    countBook++;
                else
                    countNotBook++;
            }
            var numOfUser = _context.Logins;
            foreach (var item in numOfUser)
            {
                if (item.Roleid == 2)
                    NumberOfUsers++;
            }
            var numOfTestmonial = _context.Testmonials;
            foreach (var item in numOfTestmonial)
            {
                if (item.Status == "Accept")
                    NumberOfTestmonial++;
            }
            var cate = _context.Categories.Select(x => x.Categoryname).ToList();
            var reserve = _context.Reservations;
            List<int> data = new List<int>();
            foreach (var item in cate)
            {
                data.Add(reserve.Count(x => x.CategoryNavigation.Categoryname == item));
            }
            ViewBag.Categ = cate;
            ViewBag.pr = data;
            ViewBag.countBook = countBook;
            ViewBag.countNotBook = countNotBook;
            ViewBag.NumberOfUsers = NumberOfUsers;
            ViewBag.testmonials = NumberOfTestmonial;
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            var home = _context.Homes.ToList();
            var contact = _context.Contactus.ToList();
            var about = _context.Aboutus.ToList();
            var manangeBase = Tuple.Create<IEnumerable<Home>, IEnumerable<Contactu>, IEnumerable<Aboutu>>(home, contact, about);
            return View(manangeBase);
        }
        public async Task<IActionResult> HomeUser()
        {
             int countBook = 0;
            int countNotBook = 0;
            int NumberOfUsers = 0;
            
            var bAndNReserve = _context.Reservations;
            foreach (var item in bAndNReserve)
            {
                if (item.Status == "Full")
                    countBook++;
                else
                    countNotBook++;
            }
            var numOfUser = _context.Logins;
            foreach (var item in numOfUser)
            {
                if (item.Roleid == 2)
                    NumberOfUsers++;
            }
            ViewBag.countBook = countBook;
            ViewBag.countNotBook = countNotBook;
            ViewBag.NumberOfUsers = NumberOfUsers;
            ViewBag.testmonials = _context.Testmonials.Count();
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");

            var home = _context.Homes.ToList();
            var reserve = _context.Reservations.ToList();
            var manangeBase = Tuple.Create<IEnumerable<Home>, IEnumerable<Reservation>>(home, reserve);
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var modelContext = _context.Homes.Include(h => h.Role).Include(h => h.UsernameNavigation);
            return View(manangeBase);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .Include(h => h.Role)
                .Include(h => h.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Sliderimage == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titleslider1,Descriptionslider1,Titleslider2,Descriptionslider2,Titleslider3,Descriptionslider3,Sliderimage,Video,Titleservice1,Descriptionservice1,Titleservice2,Descriptionservice2,Titleservice3,Descriptionservice3,Roleid,Username, ImageFile")] Home home)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPath = webHostEnviroment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName;
                string extension = Path.GetExtension(home.ImageFile.FileName);

                string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await home.ImageFile.CopyToAsync(filestream);
                }
                home.Video = fileName;
                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", home.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", home.Username);
            return View(home);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", home.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", home.Username);
            return View(home);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Titleslider1,Descriptionslider1,Titleslider2,Descriptionslider2,Titleslider3,Descriptionslider3,Sliderimage,Video,Titleservice1,Descriptionservice1,Titleservice2,Descriptionservice2,Titleservice3,Descriptionservice3,Roleid,Username, ImageFile")] Home home)
        {
            if (id != home.Sliderimage)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (home.ImageFile != null)
                    {
                        string wwwrootPath = webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName;
                        //string extension = Path.GetExtension(reservation.ImageFile.FileName);
                        string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await home.ImageFile.CopyToAsync(filestream);
                        }

                        home.Video = fileName;
                    }
                    else
                    {
                        home.Video = _context.Homes.Where(x => x.Video == id).AsNoTracking<Home>().FirstOrDefault().Video;

                    }
                    _context.Update(home);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Sliderimage))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", home.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", home.Username);
            return View(home);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .Include(h => h.Role)
                .Include(h => h.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Sliderimage == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var home = await _context.Homes.FindAsync(id);
            _context.Homes.Remove(home);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool HomeExists(string id)
        {
            return _context.Homes.Any(e => e.Sliderimage == id);
        }
    }
}
