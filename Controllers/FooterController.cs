using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;

namespace FirstProject.Controllers
{
    public class FooterController : Controller
    {
        private readonly ModelContext _context;

        public FooterController(ModelContext context)
        {
            _context = context;
        }

        // GET: Footer
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Headers.Include(f => f.Role).Include(f => f.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Footer/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footer = await _context.Headers
                .Include(f => f.Role)
                .Include(f => f.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Rights == id);
            if (footer == null)
            {
                return NotFound();
            }

            return View(footer);
        }

        // GET: Footer/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username");
            return View();
        }

        // POST: Footer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rights,Title1,Title2,Title3,Title4,Contact1,Contact2,Contact3,Contact4,News,Roleid,Username")] Footer footer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(footer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", footer.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", footer.Username);
            return View(footer);
        }

        // GET: Footer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footer = await _context.Headers.FindAsync(id);
            if (footer == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", footer.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", footer.Username);
            return View(footer);
        }

        // POST: Footer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Rights,Title1,Title2,Title3,Title4,Contact1,Contact2,Contact3,Contact4,News,Roleid,Username")] Footer footer)
        {
            if (id != footer.Rights)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(footer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FooterExists(footer.Rights))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", footer.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", footer.Username);
            return View(footer);
        }

        // GET: Footer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footer = await _context.Headers
                .Include(f => f.Role)
                .Include(f => f.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Rights == id);
            if (footer == null)
            {
                return NotFound();
            }

            return View(footer);
        }

        // POST: Footer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var footer = await _context.Headers.FindAsync(id);
            _context.Headers.Remove(footer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FooterExists(string id)
        {
            return _context.Headers.Any(e => e.Rights == id);
        }
    }
}
