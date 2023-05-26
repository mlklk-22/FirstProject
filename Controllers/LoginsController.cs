using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;
using Microsoft.AspNetCore.Http;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

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

        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            var modelContext = _context.Logins.Include(l => l.Role);
            return View(await modelContext.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

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
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Firstname,Lastname,Phonenumber,Email,Password,Roleid")] Login login)
        { 
            if (ModelState.IsValid)
            {
                var adminInfo = _context.Logins.Where(x => x.Roleid == 1).FirstOrDefault();
                login.Roleid = 2;
                _context.Add(login);
                await _context.SaveChangesAsync();

                #region Sending Email To User
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("Raghadbadareen7300@gmail.com"));
                email.To.Add(MailboxAddress.Parse(login.Email));



                email.Subject = "New Account :)";
                email.Body = new TextPart(TextFormat.Text)
                {
                    Text = "Ms / Mrs " + login.Firstname+ " " + login.Lastname
                                                         + " Thank You For Regisrting For Us \n"
                                                         + "Welcome To Your Website Have a fun easily Services! \n"
                                                         + "All Respect Hall Reservation\n"
                                                         + "With all love " + adminInfo.Firstname + " " + adminInfo.Lastname

                };


                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.outlook.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("Raghadbadareen7300@gmail.com", "RoRO1234");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                #endregion

                return RedirectToAction("Login", "LoginAndRegistration");
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", login.Roleid = 1);
            return View(login);
        }

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
