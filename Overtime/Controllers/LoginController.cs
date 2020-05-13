using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Overtime.Models;
using Overtime.Services;

namespace Overtime.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUser iuser;
        public LoginController(IUser _iuser)
        {
            iuser = _iuser;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                User newuser=iuser.getUserbyUsername(user.u_name);
                if (newuser.u_password.Equals(user.u_password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }
    }
}