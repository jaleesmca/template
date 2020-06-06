using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Newtonsoft.Json;
using NHibernate.SqlCommand;
using Overtime.Models;
using Overtime.Services;

namespace Overtime.Controllers
{
    public class RoleMenuController : Controller
    {
        public readonly IRole irole;
        public readonly IMenu imenu;
        public readonly IRoleMenu iroleMenu;
        public RoleMenuController(IRole _irole,IMenu _imenu,IRoleMenu _iroleMenu)
        {
            irole = _irole;
            imenu = _imenu;
            iroleMenu = _iroleMenu;
        }

        // GET: RoleMenu
        public ActionResult Index()
        {
            ViewBag.RoleList = (irole.GetRoles);
            return View();
        }

        // GET: RoleMenu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleMenu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleMenu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoleMenu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleMenu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleMenu/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public String showRoleMenus(int role,string type )
        {
            MenuWrapper menuWrapper = new MenuWrapper();
            IEnumerable<Menu> AllMenu = imenu.getMenuListNotMappedByRoleAndType(role, type);
            IEnumerable<Menu> menus = imenu.getMenulistByRoleAndType(role,type);
            menuWrapper.allMenu = AllMenu;
            menuWrapper.userMenu = menus;
            return JsonConvert.SerializeObject(menuWrapper);
        }

        [HttpPost]
        public String saveMenuitems(int role, string type,string menus)
        {

            if (menus!=null)
            {
                int Count = iroleMenu.getCountOfRoleMenuByRoleAndType(role, type);
                if (Count !=0)
                {
                    
                    iroleMenu.RemoveAllRoleMenu(role, type);
                    
                }
                Count = iroleMenu.getCountOfRoleMenuByRoleAndType(role, type);
               
                String[] array = menus.Split(',');
               

                foreach (var item in array)
                {
                    if (item != null)
                    {
                        RoleMenu roleMenu1 = iroleMenu.GetRoleMenusByRoleAndMenu(role, int.Parse(item));
                        if (roleMenu1 == null)
                        {
                            RoleMenu roleMenu = new RoleMenu();
                            roleMenu.rm_role_id = role;
                            roleMenu.rm_menu_id = int.Parse(item);
                            roleMenu.rm_active_yn = "Y";
                            roleMenu.rm_cre_by = getCurrentUser().u_id;
                            roleMenu.rm_cre_date = DateTime.Now;
                            iroleMenu.Add(roleMenu);
                        }
                    }
                }
            }
            else
            {
                int Count = iroleMenu.getCountOfRoleMenuByRoleAndType(role, type);
                if (Count != 0)
                {
                    
                    iroleMenu.RemoveAllRoleMenu(role, type);

                }
            }
            
            return "success";
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
                    ViewBag.Name = user.u_full_name;
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

    }
}