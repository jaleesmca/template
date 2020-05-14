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
    public class OvertimeRequestController : Controller
    {
        private readonly IOverTimeRequest ioverTimeRequest;
        private readonly IWorkflowDetail iworkflowDetail;
        private readonly IWorkflowTracker iworkflowTracker;

        public OvertimeRequestController(IOverTimeRequest _ioverTimeReques, IWorkflowDetail _iworkflowDetail, IWorkflowTracker _iworkflowTracker)
        {
            ioverTimeRequest = _ioverTimeReques;
            iworkflowDetail= _iworkflowDetail;
            iworkflowTracker= _iworkflowTracker;
        }

        // GET: OvertimeRequest
        public ActionResult Index()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(ioverTimeRequest.getMyOvertimeRequests);
            }
        }

        // GET: OvertimeRequest/Details/5
        public ActionResult Details(int id)
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

        // GET: OvertimeRequest/Create
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

        // POST: OvertimeRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OverTimeRequest overTimeRequest)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    overTimeRequest.rq_doc_id = 1;
                    overTimeRequest.rq_workflow_id = 1;
                    overTimeRequest.rq_status = 0;
                    overTimeRequest.rq_cre_by = getCurrentUser().u_id;
                    overTimeRequest.rq_cre_date = DateTime.Now;
                    ioverTimeRequest.Add(overTimeRequest);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: OvertimeRequest/Edit/5
        public ActionResult Edit(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(ioverTimeRequest.GetOverTimeRequest(id));
            }
        }

        // POST: OvertimeRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
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
        }

        // GET: OvertimeRequest/Delete/5
        public ActionResult Delete(int id)
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

        // POST: OvertimeRequest/Delete/5
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
                    // TODO: Add delete logic here

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }
        [HttpPost]
        public ActionResult Send(int id,String from)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);
                WorkflowDetail workflow = iworkflowDetail.GetWorkFlowDetail(overTimeRequest.rq_workflow_id);
                WorkflowTracker workflowTracker = new WorkflowTracker();

                int nextStatus = iworkflowDetail.getNextWorkflow(overTimeRequest.rq_workflow_id, overTimeRequest.rq_status);
                overTimeRequest.rq_status = nextStatus;
                ioverTimeRequest.Update(overTimeRequest);
                return RedirectToAction(from);
            }

        }
        public ActionResult Approval()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(ioverTimeRequest.getRequestForApprovals);
            }
        }
        [HttpPost]
        public ActionResult Reject(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);
                WorkflowDetail workflow = iworkflowDetail.GetWorkFlowDetail(overTimeRequest.rq_workflow_id);
                WorkflowTracker workflowTracker = new WorkflowTracker();

                int previousStatus = iworkflowDetail.getPreviousWorkflow(overTimeRequest.rq_workflow_id, overTimeRequest.rq_status);
                overTimeRequest.rq_status = previousStatus;
                ioverTimeRequest.Update(overTimeRequest);
                return RedirectToAction("Approval");
            }

        }
        public ActionResult Reports()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(ioverTimeRequest.GetOvertimeRequests);
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