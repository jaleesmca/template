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
    public class WorkflowDetailController : Controller
    {
        private readonly IWorkflowDetail iworkflowDetail;
        private readonly IMenu imenu;

        public WorkflowDetailController(IWorkflowDetail _iworkflowDetail,IMenu _imenu)
        {
            iworkflowDetail = _iworkflowDetail;
            imenu = _imenu;
        }
        // GET: WorkflowDetail
        public ActionResult Index()
        {

            return View();
        }

        // GET: WorkflowDetail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkflowDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkflowDetail/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                WorkflowDetail workflowDetail = new WorkflowDetail();
                workflowDetail.wd_role_id = Convert.ToInt32(collection["wd_role_id"]);
                workflowDetail.wd_priority = Convert.ToInt32(collection["wd_priority"]);
                workflowDetail.wd_workflow_id = Convert.ToInt32(collection["wd_workflow_id"]);
                workflowDetail.wd_cre_by = getCurrentUser().u_id;
                workflowDetail.wd_cre_date = DateTime.Now;
                workflowDetail.wd_active_yn = "Y";
                int id = workflowDetail.wd_workflow_id;
                iworkflowDetail.Add(workflowDetail);


                return View(iworkflowDetail.GetWorkFlowDetailsByWorkFlow(id));
            }
        }
    

        // GET: WorkflowDetail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkflowDetail/Edit/5
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

        // GET: WorkflowDetail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkflowDetail/Delete/5
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
        public ActionResult Status(int Workflow ,int status)
        {

            ViewBag.status = status;
            return View(iworkflowDetail.GetWorkFlowDetailsByWorkFlow(Workflow));
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