using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Overtime.Models;
using Overtime.Services;

namespace Overtime.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenu imenu;
        public MenuController(IMenu _imenu)
        {
            imenu = _imenu;
        }
        // GET: Menu
        public ActionResult Index()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(imenu.GetMenus);
            }
        }

        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            if (getCurrentUser() == null)
            {
               
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.MenuList = imenu.GetMenuList("Menu");
                return View();
            }
        }

        // POST: Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    menu.m_cre_by = getCurrentUser().u_id;
                    menu.m_cre_date = DateTime.Now;
                    imenu.Add(menu);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.MenuList = imenu.GetMenuList("Menu");
                    return View();
                }
            }
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.MenuList = imenu.GetMenuList("Menu");
                return View(imenu.GetMenu(id));
            }
        }

        // POST: Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Menu menu)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    Menu _menu = imenu.GetMenu(id);
                    _menu.m_description = _menu.m_description;
                    _menu.m_desc_to_show = menu.m_desc_to_show;
                    _menu.m_link = menu.m_link;
                    _menu.m_type = menu.m_type;
                    _menu.m_parrent_id = menu.m_parrent_id;
                    _menu.m_active_yn = menu.m_active_yn;
                    imenu.Update(_menu);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);

                    ViewBag.MenuList = imenu.GetMenuList("Menu");
                    return View();
                }
            }
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            if (getCurrentUser() == null)
            {
                
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(imenu.GetMenu(id));
            }
        }

        // POST: Menu/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    imenu.Remove(id);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
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
                    ViewBag.Name = user.u_full_name;
                    ViewBag.isAdmin = user.u_is_admin;
                    List<MenuItems> menulist = new List<MenuItems>();

                    IEnumerable<Menu> menus = imenu.getMenulistByRoleAndType(user.u_role_id, "Menu");

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
                        menuItems.menuItem = imenu.getMenulistByRoleAndTypeAndParrent(user.u_role_id, "MenuItem", menu.m_id);
                        menulist.Add(menuItems);
                    }

                    ViewBag.MenuList = menulist;
                  
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