using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System.Diagnostics;

namespace prjMvcCoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
                return View();
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CLoginViewModel vm)
        {
            DbDemoContext db = new DbDemoContext();
            TCustomer user = db.TCustomers.FirstOrDefault(c => c.FEmail.Equals(vm.txtAccount) && c.FPassword.Equals(vm.txtPassword));
            if(user!=null && user.FPassword.Equals(vm.txtPassword))
            {
                string json = JsonSerializer.Serialize(user);
                HttpContext.Session.SetString(CDictionary.SK_LOGIN_USER, json);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}