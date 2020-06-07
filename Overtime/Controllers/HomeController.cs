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
using System.Collections;

namespace Overtime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUser iuser;
        private readonly IMenu imenu;

        public HomeController(ILogger<HomeController> logger,IUser _iuser,IMenu _imenu)
        {
            _logger = logger;
            iuser = _iuser;
            imenu = _imenu;
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

                List<MenuItems> menulist =new  List<MenuItems>();

                IEnumerable<Menu> menus = imenu.getMenulistByRoleAndType(getCurrentUser().u_role_id,"Menu");
                
                foreach (var menu in menus)
                {
                    MenuItems menuItems = new MenuItems();
                    menuItems.m_id = menu.m_id;
                    menuItems.m_description = menu.m_description;
                    menuItems.m_desc_to_show = menu.m_desc_to_show;
                    menuItems.m_link = menu.m_link;
                    menuItems.m_parrent_id = menu.m_parrent_id;
                    menuItems.m_type = menu.m_type;
                    menuItems.m_cre_by = menu.m_cre_by;
                    menuItems.m_active_yn = menu.m_active_yn;
                    menuItems.m_cre_date = menu.m_cre_date;
                    menuItems.menuItem = imenu.getMenulistByRoleAndTypeAndParrent(getCurrentUser().u_role_id, "MenuItem", menu.m_id);
                    menulist.Add(menuItems);
                }

                ViewBag.MenuList = menulist;
               

                if (getCurrentUser().u_role_description.Equals("Monitor")) ViewBag.isMonitor = "Y";
                else
                {
                    ViewBag.isMonitor = "N";
                }


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
                    if (user.u_role_description.Equals("Monitor")) ViewBag.isMonitor = "Y";
                    else
                    {
                        ViewBag.isMonitor = "N";
                    }
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
