using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overtime.Models;
using Overtime.Repository;
using Overtime.Services;

namespace Overtime.Controllers
{
    public class WorkflowController : Controller
    {
        private readonly IWorkflow iworkflow;
        private readonly IRole irole;

        public WorkflowController(IWorkflow _iworkflow, IRole _irole)
        {
            iworkflow = _iworkflow;
            irole = _irole;
        }

        public ActionResult Index(int id)
        {
            ViewBag.RoleList = (irole.GetRoles);
            return View(iworkflow.GetWorkflows);
        }
        // GET: Workflow/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Workflow/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workflow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Workflow workflow)
        {
            try
            {
                iworkflow.Add(workflow);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Workflow/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Workflow/Edit/5
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

        // GET: Workflow/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Workflow/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Workflow workflow)
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
}