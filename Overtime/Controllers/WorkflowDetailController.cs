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
    public class WorkflowDetailController : Controller
    {
        private readonly IWorkflowDetail iworkflowDetail;

        public WorkflowDetailController(IWorkflowDetail _iworkflowDetail)
        {
            iworkflowDetail = _iworkflowDetail;
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
            WorkflowDetail workflowDetail = new WorkflowDetail();
            workflowDetail.wd_role_id = Convert.ToInt32(collection["wd_role_id"]);
            workflowDetail.wd_priority = Convert.ToInt32(collection["wd_priority"]);
            workflowDetail.wd_workflow_id = Convert.ToInt32(collection["wd_workflow_id"]);
            workflowDetail.wd_cre_by = 12;
            workflowDetail.wd_cre_date = DateTime.Now;
            workflowDetail.wd_active_yn = "Y";
            int id = workflowDetail.wd_workflow_id;
            iworkflowDetail.Add(workflowDetail);

           return View(iworkflowDetail.GetWorkFlowDetailsByWorkFlow(id));
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
    }
}