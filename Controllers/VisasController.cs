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
    public class VisasController : Controller
    {
        private readonly ModelContext _context;

        public VisasController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Agreement(decimal id)
        {
           
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            var user2 = ViewBag.UserName;
            var upd = (from Reservation in _context.Reservations
                       where Reservation.Username == "mlklk-22" && Reservation.Hallnumber == id
                       select Reservation).ToList();
            
            var r =0;
            var n = 0;

            foreach (var item in upd)
            {   
                n = (int)item.Price ;
                item.Username = user2;
                item.Status = "Pending";
                //foreach (var item2 in _context.Visas.Where(x => x.Username == item.Username))
                //{
                //    r = Convert.ToInt32(item2.Pocket) - n;
                //    item2.Pocket = Convert.ToString(r);
                //}
            }
            
            _context.SaveChanges();
            ViewBag.hallNum = HttpContext.Session.GetString("hallNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View(upd);
        }
        public IActionResult AgreementAcc(decimal id)
        {

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var user2 = ViewBag.UserName;
            var upd = (from Reservation in _context.Reservations
                       where Reservation.Status == "Pending" && Reservation.Hallnumber == id
                       select Reservation).ToList();

            var r = 0;
            var n = 0;

            foreach (var item in upd)
            {
                n = (int)item.Price;
                item.Username = user2;
                item.Status = "Full";
                foreach (var item2 in _context.Visas.Where(x => x.Username == item.Username))
                {
                    r = Convert.ToInt32(item2.Pocket) - n;
                    item2.Pocket = Convert.ToString(r);
                }
            }

            _context.SaveChanges();
            ViewBag.hallNum = HttpContext.Session.GetString("hallNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return RedirectToAction("ReqRes", "Visas");
        }
        public IActionResult DisAgreement(decimal id)
        {

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var user2 = ViewBag.UserName;
            
            
            var upd = (from Reservation in _context.Reservations
                       where  Reservation.Hallnumber == id
                       select Reservation).ToList();

            var r = 0;
            var n = 0;
            foreach (var item in upd)
            {
                n = (int)item.Price;
              
                    item.Status = "Available";
                    foreach (var item2 in _context.Visas.Where(x => x.Username == item.Username))
                    {
                        r = Convert.ToInt32(item2.Pocket) + n;
                        item2.Pocket = Convert.ToString(r);
                        item.Username = "mlklk-22";
                    }
              
            }

            _context.SaveChanges();
            ViewBag.hallNum = HttpContext.Session.GetString("hallNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View(upd);
        }

        public IActionResult DisAgreeAdmin(decimal id)
        {

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var user2 = ViewBag.UserName;


            var upd = (from Reservation in _context.Reservations
                       where Reservation.Hallnumber == id
                       select Reservation).ToList();

            var r = 0;
            var n = 0;
            
            foreach (var item in upd)
            {
                n = (int)item.Price;
                if (item.Status == "Pending")
                {
                    item.Status = "Available";
                    foreach (var item2 in _context.Visas.Where(x => x.Username == item.Username))
                    {
                        item.Username = "mlklk-22";
                    }
                }
                else
                {
                    item.Status = "Available";
                    foreach (var item2 in _context.Visas.Where(x => x.Username == item.Username))
                    {
                        r = Convert.ToInt32(item2.Pocket) + n;
                        item2.Pocket = Convert.ToString(r);
                        item.Username = "mlklk-22";
                    }
                }
            }

            _context.SaveChanges();
            ViewBag.hallNum = HttpContext.Session.GetString("hallNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return RedirectToAction("ReqRes", "Visas");
        }
        public IActionResult ReqRes()
        {
            //var upd = (from Reservation in _context.Reservations
            //           where Reservation.Username == user
            //           select Reservation).ToList();
            //foreach (var item in upd)
            //{
            //    item.Username = user;
            //}
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            _context.SaveChanges();
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var modelContext = _context.Reservations.Where(x => x.Username != null && x.Username != "mlklk-22");
            return View(modelContext);
        }
        public IActionResult MyVisa()
        {
           ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var modelContext = _context.Visas;  

            return View(modelContext);
        }

        public IActionResult ReqSub()
        {
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }
        public IActionResult MyReservations()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var modelContext = _context.Reservations.Where(x => x.Username != null && x.Username != "mlklk-22");
            return View(modelContext);
        }

        // GET: Visas
        public async Task<IActionResult> Index()    
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            var modelContext = _context.Visas.Include(v => v.Role).Include(v => v.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Visas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .Include(v => v.Role)
                .Include(v => v.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Cardnumber == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // GET: Visas/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username");
            return View();
        }

        // POST: Visas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Cardnumber,Exp,Thrnum,Pocket,Roleid,Username")] Visa visa)
        {
            if (ModelState.IsValid)
            {
                    HttpContext.Session.SetString("cardNum", visa.Cardnumber);
                    visa.Roleid = 2;
                     visa.Pocket = "1000";
                    visa.Username = HttpContext.Session.GetString("UserName");
                    _context.Add(visa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("HomeUser", "Homef");
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", visa.Roleid = 2);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", HttpContext.Session.GetString("UserName"));
            return View(visa);
        }

       public IActionResult Visa()
        {
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            return View();
        }
        public IActionResult ER()
        {
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            return View();
        }

        // GET: Visas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas.FindAsync(id);
            if (visa == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", visa.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", visa.Username);
            return View(visa);
        }

        // POST: Visas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Firstname,Lastname,Cardnumber,Exp,Thrnum,Pocket,Roleid,Username")] Visa visa)
        {
            if (id != visa.Cardnumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisaExists(visa.Cardnumber))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", visa.Roleid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", visa.Username);
            return View(visa);
        }

        // GET: Visas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .Include(v => v.Role)
                .Include(v => v.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Cardnumber == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // POST: Visas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var visa = await _context.Visas.FindAsync(id);
            _context.Visas.Remove(visa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisaExists(string id)
        {
            return _context.Visas.Any(e => e.Cardnumber == id);
        }
    }
}
