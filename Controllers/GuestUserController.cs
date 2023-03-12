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
}
