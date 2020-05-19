using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Overtime.Services;

namespace Overtime.Controllers
{
    public class WorkflowTrackerController : Controller
    {
        private readonly IWorkflowTracker iworkflowTracker;
        private readonly IWorkflowDetail iworkflowDetail;
        public WorkflowTrackerController(IWorkflowTracker _iworkflowTracker,IWorkflowDetail _iworkflowDetail)
        {
            iworkflowDetail = _iworkflowDetail;
            iworkflowTracker = _iworkflowTracker;
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

    }
}