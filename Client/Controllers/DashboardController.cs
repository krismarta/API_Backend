using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Client.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            
            var roleActive = "Not Found";
            if (User.IsInRole("Manager"))
            {
                roleActive = "Manager";
            }
            else if (User.IsInRole("Director"))
            {
                roleActive = "Director";
            }
            else if (User.IsInRole("Employee"))
            {
                roleActive = "Employee";
            }
            else
            {
                roleActive = "Tidak diketahui";
            }

            ViewData["role"] = HttpContext.Session.GetString("roleActive");
            ViewData["email"] = HttpContext.Session.GetString("Email");
            ViewData["Phone"] = HttpContext.Session.GetString("Phone");
            return View();
        }
    }
}
