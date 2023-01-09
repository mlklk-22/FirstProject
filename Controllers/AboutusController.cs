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

namespace FirstProject.Controllers
{
    public class AboutusController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviroment;
        public AboutusController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            this.webHostEnviroment = webHostEnviroment;
        }

       
        // GET: Aboutus
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");

            var modelContext = _context.Aboutus.Include(a => a.Role).Include(a => a.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> AboutUsUser()
        {
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var modelContext = _context.Aboutus.Include(a => a.Role).Include(a => a.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }
        // GET: Aboutus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .Include(a => a.Role)
                .Include(a => a.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Titleimage == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // GET: Aboutus/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username");
            return View();
        }

        // POST: Aboutus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title1,Descriptiontitle1,Title2,Descriptiontitle2,Title3,Descriptiontitle3,Title4,Descriptiontitle4,Titlechose1,Descriptionchose1,Titlechose2,Descriptionchose2,Titlechose3,Descriptionchose3,Titlechose4,Descriptionchose4,Titlechose5,Descriptionchose5,Titleimage,Choseimage,Roleid,Username, ImageFile")] Aboutu aboutu)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPath = webHostEnviroment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
                string extension = Path.GetExtension(aboutu.ImageFile.FileName);

                string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await aboutu.ImageFile.CopyToAsync(filestream);
                }
                aboutu.Choseimage = fileName;
                _context.Add(aboutu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", aboutu.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", aboutu.Username);
            return View(aboutu);
        }

        // GET: Aboutus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus.FindAsync(id);
            if (aboutu == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", aboutu.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", aboutu.Username);
            return View(aboutu);
        }

        // POST: Aboutus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Title1,Descriptiontitle1,Title2,Descriptiontitle2,Title3,Descriptiontitle3,Title4,Descriptiontitle4,Titlechose1,Descriptionchose1,Titlechose2,Descriptionchose2,Titlechose3,Descriptionchose3,Titlechose4,Descriptionchose4,Titlechose5,Descriptionchose5,Titleimage,Choseimage,Roleid,Username, ImageFile")] Aboutu aboutu)
        {
            if (id != aboutu.Titleimage)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutu.ImageFile != null)
                    {
                        string wwwrootPath = webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
                        //string extension = Path.GetExtension(reservation.ImageFile.FileName);
                        string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await aboutu.ImageFile.CopyToAsync(filestream);
                        }

                        aboutu.Choseimage = fileName;
                    }
                    else
                    {
                        aboutu.Choseimage = _context.Aboutus.Where(x => "nn" == id).AsNoTracking<Aboutu>().FirstOrDefault().Choseimage;

                    }
                    _context.Update(aboutu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutuExists(aboutu.Titleimage))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", aboutu.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", aboutu.Username);
            return View(aboutu);
        }

        // GET: Aboutus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .Include(a => a.Role)
                .Include(a => a.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Titleimage == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // POST: Aboutus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aboutu = await _context.Aboutus.FindAsync(id);
            _context.Aboutus.Remove(aboutu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutuExists(string id)
        {
            return _context.Aboutus.Any(e => e.Titleimage == id);
        }
    }
}
