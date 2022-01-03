
using API.Models;
using API.ViewModel;
using Client.Base.Controllers;
using Client.Repositories.Data;
using Client.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public LoginController(AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "dashboard");
            }
            return View();
        }

        [HttpPost]
        public JsonResult actionLogin(LoginResponseVM entity)
        {
            var result = accountRepository.LoginEmployee(entity);
            if (result.Result.idtoken == null)
            {
                //return RedirectToAction("index");
                result.Result.statusCode = "0";
            }
            result.Result.statusCode = "1";
            HttpContext.Session.SetString("JWToken", result.Result.idtoken);
            if (result.Result.Email != null)
            {
                HttpContext.Session.SetString("Email", result.Result.Email);
            }
            else
            {
                HttpContext.Session.SetString("Phone", result.Result.Phone);
            }
            //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");
            //return RedirectToAction("index", "dashboard");
            return Json(result);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Main");
        }

        [HttpGet("Unauthorized/")]
        public IActionResult Unauthorized() {
            return View("401");
        }
        [HttpGet("Forbidden/")]
        public IActionResult Forbidden()
        {
            return View("403");
        }
        [HttpGet("Notfound/")]
        public IActionResult Notfound()
        {
            return View("404");
        }

    }
}
