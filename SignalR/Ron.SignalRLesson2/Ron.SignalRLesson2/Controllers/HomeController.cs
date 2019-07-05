using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ron.SignalRLesson2.Models;
using System.Diagnostics;

namespace Ron.SignalRLesson2.Controllers
{
    public class HomeController : Controller
    {      

        public IActionResult Index()
        {      
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult Chat()
        {
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
