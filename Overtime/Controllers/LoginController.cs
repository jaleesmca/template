using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Overtime.Models;
using Overtime.Repository;
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
            var key = "shdfg2323g3g4j3879sdfh2j3237w8eh";
            try
            {
                if (user.u_name!=null&&user.u_password!=null)
                {
                    
                  
                    User newuser = iuser.getUserbyUsername(user.u_name);
                    if (newuser != null)
                    {
                        var newPassword = AesOperaions.DecryptString(key, newuser.u_password);

                        if (user.u_password.ToString().Equals(newPassword.ToString()))
                        {
                            newuser.u_password = null;
                            string JsonStr = JsonConvert.SerializeObject(newuser);
                            HttpContext.Session.SetString("User", JsonStr);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Message = "User Name and Password are incrrect!!!";
                            return View("Index");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "User Name and Password are incorrect!!!";
                        return View("Index");
                    }
                }else
                {
                    ViewBag.Message = "Please enter username and Password";
                    return View("Index");
                }
                
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View("Index");
            }
        }
    }
}