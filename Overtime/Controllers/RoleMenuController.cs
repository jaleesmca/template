using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overtime.Models;
using Overtime.Services;

namespace Overtime.Controllers
{
    public class RoleMenuController : Controller
    {
        public readonly IRole irole;
        public readonly IMenu imenu;
        public RoleMenuController(IRole _irole,IMenu _imenu)
        {
            irole = _irole;
            imenu = _imenu;
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
        public IEnumerable<Menu> showRoleMenus(int role,string type )
        {
            IEnumerable<Menu> menus = imenu.getMenulistByRoleAndType(role,type);

            return menus;
        }
    }
}