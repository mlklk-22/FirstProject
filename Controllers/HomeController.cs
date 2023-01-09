using FirstProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;


namespace FirstProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUS()
        {
            return View();
        }

        public IActionResult Testimonial()
        {
            return View();
        }
        [HttpGet]
      
        public IActionResult Dashboard()
        {
            int countBook = 0;
            int countNotBook = 0;
            int NumberOfUsers = 0;
            int NumberOfTestmonial = 0;

            var bAndNReserve = _context.Reservations;
            foreach (var item in bAndNReserve)
            {
                if (item.Status == "Full")
                    countBook++;
                else
                    countNotBook++;
            }
            var numOfUser = _context.Logins;
            foreach (var item in numOfUser)
            {
                if (item.Roleid == 2)
                    NumberOfUsers++;
            }
            var numOfTestmonial = _context.Testmonials;
            foreach (var item in numOfTestmonial)
            {
                if (item.Status == "Accept")
                    NumberOfTestmonial++;
            }
            var cate = _context.Categories.Select(x => x.Categoryname).ToList();
            var reserve = _context.Reservations;
            List<int> data = new List<int>();
            foreach (var item in cate)
            {
               data.Add(reserve.Count(x => x.CategoryNavigation.Categoryname == item));
            }
            ViewBag.Categ = cate;
            ViewBag.pr = data;
            ViewBag.countBook = countBook;
            ViewBag.countNotBook = countNotBook;
            ViewBag.NumberOfUsers = NumberOfUsers;
            ViewBag.testmonials = NumberOfTestmonial;
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.LastName = HttpContext.Session.GetString("LastName");
            var home = _context.Homes.ToList();
            var contact = _context.Contactus.ToList();
            var about = _context.Aboutus.ToList();
            var manangeBase = Tuple.Create<IEnumerable<Home>, IEnumerable<Contactu>, IEnumerable<Aboutu>>(home, contact, about);
            return View(manangeBase);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
