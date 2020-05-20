using System;
using System.Collections.Generic;
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
    public class WorkflowController : Controller
    {
        private readonly IWorkflow iworkflow;
        private readonly IRole irole;
        private readonly IWorkflowDetail iworkflowDetail;

        public WorkflowController(IWorkflow _iworkflow, IRole _irole, IWorkflowDetail _iworkflowDetail)
        {

            iworkflow = _iworkflow;
            irole = _irole;
            iworkflowDetail = _iworkflowDetail;
        }

        public ActionResult Index(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.RoleList = (irole.GetRoles);
                return View(iworkflow.GetWorkflows);
            }
        }
        // GET: Workflow/Details/5
        public ActionResult Details(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(iworkflowDetail.GetWorkFlowDetailsByWorkFlow(id));
            }
        }

        // GET: Workflow/Create
        public ActionResult Create()
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

        // POST: Workflow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Workflow workflow)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    workflow.w_active_yn = "Y";
                    workflow.w_cre_by = getCurrentUser().u_id;
                    workflow.w_cre_date = DateTime.Now;
                    iworkflow.Add(workflow);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Workflow/Edit/5
        public ActionResult Edit(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(iworkflow.GetWorkflow(id));
            }
        }

        // POST: Workflow/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Workflow workflow)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    Workflow workflow1 = iworkflow.GetWorkflow(id);
                    workflow1.w_description = workflow.w_description;
                    iworkflow.Update(workflow1);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Workflow/Delete/5
        public ActionResult Delete(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(iworkflow.GetWorkflow(id));
            }
        }

        // POST: Workflow/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Workflow workflow)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    iworkflow.Remove(id);

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
                    ViewBag.Name = user.u_name;
                    ViewBag.isAdmin = user.u_is_admin;
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