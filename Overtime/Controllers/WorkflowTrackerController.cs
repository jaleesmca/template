using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Overtime.Models;
using Overtime.Services;

namespace Overtime.Controllers
{
    public class WorkflowTrackerController : Controller
    {
        private readonly IWorkflowTracker iworkflowTracker;
        private readonly IWorkflowDetail iworkflowDetail;
        private readonly IMenu imenu;
        public WorkflowTrackerController(IWorkflowTracker _iworkflowTracker,IWorkflowDetail _iworkflowDetail,IMenu _imenu)
        {
            iworkflowDetail = _iworkflowDetail;
            iworkflowTracker = _iworkflowTracker;
            imenu = _imenu;
        }
        // GET: WorkflowTracker
        public ActionResult Index()
        {
            return View();
        }

        // GET: WorkflowTracker/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkflowTracker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkflowTracker/Create
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

        // GET: WorkflowTracker/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkflowTracker/Edit/5
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

        // GET: WorkflowTracker/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkflowTracker/Delete/5
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
        public ActionResult History(int rowid, int doc_id, int workflow)
        {
            
            return View(iworkflowTracker.GetWorkflowTrackersbyDocument(rowid,doc_id,workflow));
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