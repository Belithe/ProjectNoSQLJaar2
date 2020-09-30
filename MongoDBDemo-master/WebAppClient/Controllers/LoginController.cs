using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAppClient.Controllers
{
    public class LoginController : Controller
    {
        public string username;
        public string password;
        public IActionResult Index()
        {
            return View();
        }
        public RedirectToActionResult Submit()
        {
            username = this.Request.Form["loginEmailId"];
            return RedirectToAction("Index", "Home");

            
        }

        
    }
}