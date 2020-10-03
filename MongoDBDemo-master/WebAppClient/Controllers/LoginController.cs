using Marvin.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppClient.Helpers;
using WebAppClient.Models;
using WebAppClient.ViewModels;

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

        [HttpPost]
        public async Task<IActionResult> Verify(User user)
        {
            username = user.EmailAdress; 

            HttpClient client = MVCClientHttpClient.GetClient();
            HttpResponseMessage userResponse = await client.GetAsync("api/user/");

            UsersVM AllUsersVM = new UsersVM();

            if (userResponse.IsSuccessStatusCode)
            {
                string Content = await userResponse.Content.ReadAsStringAsync();
                AllUsersVM.lstUser = JsonConvert.DeserializeObject<IEnumerable<User>>(Content);
            }
            else
            {
                return Content("An error occurred.");
            }

            if (AllUsersVM.lstUser.Any(u => u.EmailAdress == user.EmailAdress && u.Password == user.Password)) {
                return View("~/Views/Home/Index.cshtml", AllUsersVM);
            } else
            {
                return View("Index");
            }

        }

        //public async Task<IActionResult> Submit()
        //{
        //    username = this.Request.Form["loginEmail"];
        //    password = this.Request.Form["loginPass"];

        //    HttpClient client = MVCClientHttpClient.GetClient();
        //    HttpResponseMessage userResponse = await client.GetAsync("api/user/");

        //    UsersVM AllUsersVM = new UsersVM();
        //    if (userResponse.IsSuccessStatusCode)
        //    {
        //        string Content = await userResponse.Content.ReadAsStringAsync();
        //        AllUsersVM.lstUser = JsonConvert.DeserializeObject<IEnumerable<User>>(Content);
        //    }
        //    else
        //    {
        //        Content("An error occurred.");
        //    }

        //    foreach (User user in AllUsersVM.lstUser) 
        //    {
        //        if(username == user.EmailAdress)
        //        {
        //            if(password == user.Password)
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            return RedirectToAction("Index", "Login");
        //        }
        //    }
        //    return RedirectToAction("Index", "Login");

            
        //}

        
    }
}