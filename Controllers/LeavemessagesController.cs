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
    public class LeavemessagesController : Controller
    {
        private readonly ModelContext _context;

        public LeavemessagesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Leavemessages
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            var modelContext = _context.Leavemessages.Include(l => l.Role).Include(l => l.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Leavemessages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavemessage = await _context.Leavemessages
                .Include(l => l.Role)
                .Include(l => l.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Email == id);
            if (leavemessage == null)
            {
                return NotFound();
            }

            return View(leavemessage);
        }

        // GET: Leavemessages/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username");
            return View();
        }

        // POST: Leavemessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,Subject,Message,Roleid,Username")] Leavemessage leavemessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leavemessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", leavemessage.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", leavemessage.Username);
            return View(leavemessage);
        }

        // GET: Leavemessages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavemessage = await _context.Leavemessages.FindAsync(id);
            if (leavemessage == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", leavemessage.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", leavemessage.Username);
            return View(leavemessage);
        }

        // POST: Leavemessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Firstname,Lastname,Email,Subject,Message,Roleid,Username")] Leavemessage leavemessage)
        {
            if (id != leavemessage.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leavemessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeavemessageExists(leavemessage.Email))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", leavemessage.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", leavemessage.Username);
            return View(leavemessage);
        }

        // GET: Leavemessages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavemessage = await _context.Leavemessages
                .Include(l => l.Role)
                .Include(l => l.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Email == id);
            if (leavemessage == null)
            {
                return NotFound();
            }

            return View(leavemessage);
        }

        // POST: Leavemessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var leavemessage = await _context.Leavemessages.FindAsync(id);
            _context.Leavemessages.Remove(leavemessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeavemessageExists(string id)
        {
            return _context.Leavemessages.Any(e => e.Email == id);
        }
    }
}
