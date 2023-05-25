using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;
using Microsoft.AspNetCore.Http;

namespace FirstProject.Controllers
{
    public class TestmonialsController : Controller
    {
        private readonly ModelContext _context;

        public TestmonialsController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            var modelContext = _context.Testmonials.Include(t => t.Role).Include(t => t.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> TestmonialUser()
        {
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var modelContext = _context.Testmonials.Include(t => t.Role).Include(t => t.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testmonial = await _context.Testmonials
                .Include(t => t.Role)
                .Include(t => t.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Testmonialid == id);
            if (testmonial == null)
            {
                return NotFound();
            }

            return View(testmonial);
        }
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Message,Status,Roleid,Username,Testmonialid")] Testmonial testmonial, string FirstName, string LastName, string Message)
        {
            if (ModelState.IsValid)
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                testmonial.Firstname = FirstName;
                testmonial.Lastname = LastName;
                testmonial.Message = Message;
                testmonial.Status = "Pending";
                testmonial.Roleid = 2;
                testmonial.Username = ViewBag.UserName;
                ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", testmonial.Roleid);
                ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", testmonial.Username);
                _context.Add(testmonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TestmonialUser));
            }
           
            return View(testmonial);
        }
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testmonial = await _context.Testmonials.FindAsync(id);
            if (testmonial == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", testmonial.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", testmonial.Username);
            return View(testmonial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Firstname,Lastname,Message,Status,Roleid,Username,Testmonialid")] Testmonial testmonial)
        {
            if (id != testmonial.Testmonialid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testmonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestmonialExists(testmonial.Testmonialid))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", testmonial.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", testmonial.Username);
            return View(testmonial);
        }
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testmonial = await _context.Testmonials
                .Include(t => t.Role)
                .Include(t => t.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Testmonialid == id);
            if (testmonial == null)
            {
                return NotFound();
            }

            return View(testmonial);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var testmonial = await _context.Testmonials.FindAsync(id);
            _context.Testmonials.Remove(testmonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestmonialExists(decimal id)
        {
            return _context.Testmonials.Any(e => e.Testmonialid == id);
        }
    }
}
