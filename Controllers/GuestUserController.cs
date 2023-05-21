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
    public class GuestUserController : Controller
    {
        private readonly ModelContext _context;

        public GuestUserController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> HomeGuestUser()
        {
            ViewBag.NoUsers = _context.Logins.Where(x => x.Roleid == 2).Count();
            ViewBag.NoBook = _context.Reservations.Where(x => x.Status == status.Full.ToString()).Count();
            ViewBag.NoUnBook = _context.Reservations.Where(x => x.Status == status.Available.ToString()).Count();
            ViewBag.test = _context.Testmonials.Where(x => x.Status == statusTestmonial.Accept.ToString()).Count();
            var home = _context.Homes.ToList();
            var reserve = _context.Reservations.ToList();
            var manangeBase = Tuple.Create<IEnumerable<Home>, IEnumerable<Reservation>>(home, reserve);
            return View(manangeBase);
        }
        public async Task<IActionResult> AboutUsGuestUser()
        {
            var modelContext = _context.Aboutus.Include(a => a.Role).Include(a => a.UsernameNavigation);
            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> TestmonialGuestUser()
        {
            var modelContext = _context.Testmonials.Include(t => t.Role).Include(t => t.UsernameNavigation);
            return View(await modelContext.ToListAsync());

        }
        public async Task<IActionResult> ContactUsGuestUser()
        {
            var modelContext = _context.Contactus.Include(c => c.Role).Include(c => c.UsernameNavigation);
            return View(await modelContext.ToListAsync());

        }


    }
    enum status
    {
        Available,
        Pending,
        Full
    }
    enum statusTestmonial
    {
        Accept,
        Pending
    }
}
