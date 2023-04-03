using System;
using MimeKit;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using MailKit.Security;

namespace FirstProject.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviroment;
        public ReservationsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            this.webHostEnviroment = webHostEnviroment;
        }
        [HttpGet]
        public IActionResult AccEm()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        [HttpPost]/*string firstName,*/
        public IActionResult AccEm(string firstName, string LastName, string Email, string Subject, string Message)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("mlk", "saba_tradat26@yahoo.com");
            message.From.Add(from);
            MailboxAddress to = new MailboxAddress("mlk", "mlkmsbh84@outlook.com");
            message.To.Add(to);
            message.Subject = "Reset";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
            "<h1 style=\"color:pink\">Tahaluf elemart</h1>" + "Regards" + "<p>hello</p>";
            message.Body = bodyBuilder.ToMessageBody();
            using (var clinte = new SmtpClient())
            {
                clinte.Connect("smtp.mail.yahoo.com", 465, true);
                clinte.Authenticate("saba_tradat26@yahoo.com", "gbetdprsqepmofdp");
                clinte.Send(message);
                clinte.Disconnect(true);
            }
            return View();

        }


    // GET: Reservations
    public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            ViewBag.NumberOfUsers = _context.Logins.Count();
            var modelContext = _context.Reservations.Include(r => r.CategoryNavigation).Include(r => r.UsernameNavigation);
            if (startDate == null && endDate == null )
            {
                return View(modelContext);
            }
            else if (startDate == null && endDate != null)
            {

                var result = await modelContext.Where(x => x.Timefrom.Date == endDate).ToListAsync();
                return View(result);
            }
            else if (startDate != null && endDate == null)
            {
                var result = await modelContext.Where(x => x.Timeto.Date == startDate).ToListAsync();
                return View(result);

            }
            else
            {
                var result = await modelContext.Where(x => x.Timefrom >= startDate && x.Timeto <= endDate).ToListAsync();
                return View(result);
            }
          //  return View(await modelContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Reservations.Include(r => r.CategoryNavigation).Include(r => r.UsernameNavigation);
            return View(modelContext);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            

            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.CategoryNavigation)
                .Include(r => r.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Hallnumber == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["Categoryid"] = new SelectList(_context.Categories, "Categoryid", "Categoryname");
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hallnumber,City,Place,Category,Status,Timefrom,Timeto,Day,Price,Username,Categoryid, ImageFile")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPath = webHostEnviroment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + reservation.ImageFile.FileName;
                string extension = Path.GetExtension(reservation.ImageFile.FileName);

                string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await reservation.ImageFile.CopyToAsync(filestream);
                }
                reservation.Image = fileName;
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoryid"] = new SelectList(_context.Categories, "Categoryid", "Categoryname", reservation.Categoryid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", reservation.Username);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Categories, "Categoryid", "Categoryname", reservation.Categoryid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", reservation.Username);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Hallnumber,City,Place,Category,Status,Timefrom,Timeto,Day,Price,Username,Categoryid,Description , Image, ImageFile")] Reservation reservation)
        {
            if (id != reservation.Hallnumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (reservation.ImageFile != null)
                    {
                        string wwwrootPath = webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + reservation.ImageFile.FileName;
                        //string extension = Path.GetExtension(reservation.ImageFile.FileName);
                        string path = Path.Combine(wwwrootPath + "/Images/" + fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await reservation.ImageFile.CopyToAsync(filestream);
                        }

                        reservation.Image = fileName;
                    }
                    else
                    {
                        reservation.Image = _context.Reservations.Where(x => x.Hallnumber == id).AsNoTracking<Reservation>().FirstOrDefault().Image;

                    }

                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Hallnumber))
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
            ViewData["Categoryid"] = new SelectList(_context.Categories, "Categoryid", "Categoryname", reservation.Categoryid);
            ViewData["Username"] = new SelectList(_context.Logins, "Username", "Username", reservation.Username);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.CategoryNavigation)
                .Include(r => r.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Hallnumber == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ReservationExists(decimal id)
        {
            return _context.Reservations.Any(e => e.Hallnumber == id);
        }

[HttpPost]
        public async Task<IActionResult> Search(string place, string city)
        {
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            var modelContext = _context.Reservations;
            if (place == null && city == null) 
            {
                return View(modelContext); 
            }
            else if (place != null && city == null)
            {

                var result = await modelContext.Where(x => x.Place == place).ToListAsync();
                return View(result);
            }
            else if (place == null && city != null)
            {
                var result = await modelContext.Where(x => x.City == city).ToListAsync(); 
                return View(result);

            }
            else {
                var result = await modelContext.Where(x => x.Place == place && x.City == city).ToListAsync();
                return View(result); 
            }
        }

      
         [HttpGet] 
        public IActionResult Search()
        {
            ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            var modelContext = _context.Reservations;
            return View(modelContext);
        }


    }
}
