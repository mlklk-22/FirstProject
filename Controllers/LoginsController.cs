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
    public class LoginsController : Controller
    {
        private readonly ModelContext _context;

        public LoginsController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Profile(string id)
        {
            HttpContext.Session.GetString("UserName");

            if (id == null)
            {
                return NotFound();
            }


            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename",  2);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(string id, [Bind("Username,Firstname,Lastname,Phonenumber,Email,Password,Roleid")] Login login)
        {
            HttpContext.Session.GetString("UserName");
            if (id != login.Username)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    login.Roleid = 2;
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Username))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("HomeUser", "Homef");
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", 2);
            return View(login);
        }


        // GET: Logins
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            var modelContext = _context.Logins.Include(l => l.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("AdminName");

            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.Role)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Firstname,Lastname,Phonenumber,Email,Password,Roleid")] Login login)
        { 
            if (ModelState.IsValid)
            {
                login.Roleid = 2;
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "LoginAndRegistration");
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", login.Roleid = 1);
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            if (id == null)
            {
                return NotFound();
            }
            

            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", login.Roleid);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Firstname,Lastname,Phonenumber,Email,Password,Roleid")] Login login)
        {

            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            if (id != login.Username)
            {
                return NotFound();
            }
            if (id == "mjeed_musabeh")
            {
                login.Roleid = 2;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Username))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", login.Roleid);
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.Role)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var login = await _context.Logins.FindAsync(id);
            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(string id)
        {
            return _context.Logins.Any(e => e.Username == id);
        }
    }
}
