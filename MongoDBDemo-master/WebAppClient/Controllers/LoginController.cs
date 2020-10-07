using Marvin.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Principal;
using WebAppClient.Helpers;
using WebAppClient.Models;
using WebAppClient.ViewModels;
using static System.Net.WebRequestMethods;

namespace WebAppClient.Controllers
{
    public class LoginController : Controller
    {
        public string errorMessage = "";
        public IActionResult Index()
        {
            //if (!string.IsNullOrEmpty((string)TempData["ErrorMessage"]))
            //{
            //    errorMessage = (string)TempData["ErrorMessage"];
            //}
            //ViewBag.Error = errorMessage;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Verify(User user)
        { 

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

            try
            {               
                HttpContext.User = new GenericPrincipal(new GenericIdentity(user.EmailAdress), new string[] { AllUsersVM.lstUser.Where(u => u.EmailAdress == user.EmailAdress && u.Password == user.Password).ToList()[0].Type.ToString() });
                return View("~/Views/Home/Index.cshtml", AllUsersVM);
            }
            catch
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