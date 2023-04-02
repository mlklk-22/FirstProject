using Microsoft.AspNetCore.Mvc; 
using FirstProject.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FirstProject.Controllers
{

    public class LoginAndRegistrationController : Controller
    {
        private readonly ModelContext _context;

        public LoginAndRegistrationController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Login([Bind("Username, Password")] Login login)
        {
            var auth = _context.Logins.Where(x => x.Username ==  login.Username && x.Password == login.Password).SingleOrDefault();
            if (auth != null)
            {           

                switch (auth.Roleid)
                {
                    case 1:
                       // HttpContext.Session.SetInt32("UserId", (int)auth.);
                        HttpContext.Session.SetString("AdminName", auth.Username);
                        HttpContext.Session.SetString("FirstName", auth.Firstname);
                        HttpContext.Session.SetString("LastName", auth.Lastname);
                        return RedirectToAction("Dashboard", "Homef");
                        

                        case 2:
                        // HttpContext.Session.SetInt32("UserId", (int)auth.);
                        HttpContext.Session.SetString("UserName", auth.Username);
                        HttpContext.Session.SetString("FirstName", auth.Firstname);
                        HttpContext.Session.SetString("LastName", auth.Lastname);
                        ViewBag.cardNum = HttpContext.Session.GetString("cardNum");
                        return RedirectToAction("HomeUser", "Homef");


                }
            }
          
                            return View();

       
        }

        public IActionResult LogOut([Bind("Username, Password")] Login login)
        {
            var auth = _context.Logins.Where(x => x.Username == login.Username && x.Password == login.Password).SingleOrDefault();
            if (auth != null)
            {

                switch (auth.Roleid)
                {
                    case 1:
                        // HttpContext.Session.SetInt32("UserId", (int)auth.);
                        HttpContext.Session.Clear();
                        return RedirectToAction("Login", "LoginAndRegistration");


                    case 2:
                        // HttpContext.Session.SetInt32("UserId", (int)auth.);
                        HttpContext.Session.Clear();
                        return RedirectToAction("Login", "LoginAndRegistration");


                }
            }

            return View();
        }


    }
}
