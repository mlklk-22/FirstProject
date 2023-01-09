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
    public class ContactusController : Controller
    {
        private readonly ModelContext _context;

        public ContactusController(ModelContext context)
        {
            _context = context;
        }

        // GET: Contactus
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");

            var modelContext = _context.Contactus.Include(c => c.Role).Include(c => c.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }

        public async Task<IActionResult> ContactUsUser()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
            var modelContext = _context.Contactus.Include(c => c.Role).Include(c => c.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }
        // GET: Contactus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactu = await _context.Contactus
                .Include(c => c.Role)
                .Include(c => c.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Mailtitle == id);
            if (contactu == null)
            {
                return NotFound();
            }

            return View(contactu);
        }

        // GET: Contactus/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username");
            return View();
        }

        // POST: Contactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Visittitle,Descriptionvisit,Mailtitle,Descriptionmail,Call,Descriptioncall,Work,Descriptionwork,Roleid,Username")] Contactu contactu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", contactu.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", contactu.Username);
            return View(contactu);
        }

        // GET: Contactus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactu = await _context.Contactus.FindAsync(id);
            if (contactu == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", contactu.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", contactu.Username);
            return View(contactu);
        }

        // POST: Contactus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Visittitle,Descriptionvisit,Mailtitle,Descriptionmail,Call,Descriptioncall,Work,Descriptionwork,Roleid,Username")] Contactu contactu)
        {
            if (id != contactu.Mailtitle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactuExists(contactu.Mailtitle))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", contactu.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", contactu.Username);
            return View(contactu);
        }

        // GET: Contactus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactu = await _context.Contactus
                .Include(c => c.Role)
                .Include(c => c.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Mailtitle == id);
            if (contactu == null)
            {
                return NotFound();
            }

            return View(contactu);
        }

        // POST: Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var contactu = await _context.Contactus.FindAsync(id);
            _context.Contactus.Remove(contactu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactuExists(string id)
        {
            return _context.Contactus.Any(e => e.Mailtitle == id);
        }
    }
}
