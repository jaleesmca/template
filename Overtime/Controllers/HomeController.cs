using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Overtime.Models;
using Overtime.Services;
using Overtime.Repository;

namespace Overtime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUser iuser;

        public HomeController(ILogger<HomeController> logger,IUser _iuser)
        {
            _logger = logger;
            iuser = _iuser;
        }

        public IActionResult Index()
        {
            if(getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }else
            {

                ViewBag.Name = getCurrentUser().u_full_name;
                ViewBag.isAdmin = getCurrentUser().u_is_admin;
               

                return View();
            }
   
        }

        private User getCurrentUser()
        {
            try
            {
                if (HttpContext.Session.GetString("User") == null)
                {
                    return null;
                }
                else
                {
                    User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
                    ViewBag.Name =user.u_full_name;
                    ViewBag.isAdmin = user.u_is_admin;
                    return user;
                }

            }
            catch
            {
                return null;
            }
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
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("User", "");
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Reset()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }
        public IActionResult ChangePassword(string u_password,string u_confirm)
        {

            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (u_password.Equals(u_confirm))
                {
                    User user = getCurrentUser();
                    var key = "shdfg2323g3g4j3879sdfh2j3237w8eh";
                    var encryptedString = AesOperaions.EncryptString(key, u_password);
                    user.u_password = encryptedString.ToString();
                    iuser.Update(user);
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.Error = "Your password and confirmation password do not match!!";
                    return RedirectToAction("Reset");
                }

            }
        }
    }
}
