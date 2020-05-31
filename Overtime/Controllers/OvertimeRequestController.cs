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
        private readonly IDepartment idepartment;
        private readonly IDocuments idocuments;
        private readonly IRole irole;
        private readonly IUser iuser;

        public OvertimeRequestController(IOverTimeRequest _ioverTimeReques, IWorkflowDetail _iworkflowDetail,
            IWorkflowTracker _iworkflowTracker,IDepartment _idepartment,IDocuments _idocuments,IRole _irole,IUser _iuser)
        {
            ioverTimeRequest = _ioverTimeReques;
            iworkflowDetail= _iworkflowDetail;
            iworkflowTracker= _iworkflowTracker;
            idepartment = _idepartment;
            idocuments = _idocuments;
            irole = _irole;
            iuser = _iuser;
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
                ViewBag.MyOnProcessRequests=ioverTimeRequest.GetMyOnProcessRequests(getCurrentUser().u_id);
                return View(ioverTimeRequest.GetMyOvertimeRequests(getCurrentUser().u_id));
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
                ViewBag.DepartmentList = (idepartment.GetDepartments);
                return View();
            }
        }

        // POST: OvertimeRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OverTimeRequest overTimeRequest)
        {
            if (ModelState.IsValid)
            {
                if (getCurrentUser() == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    try
                    {
                        Documents documents = idocuments.GetDocument(1);
                        overTimeRequest.rq_doc_id = documents.dc_id;
                        overTimeRequest.rq_workflow_id = documents.dc_workflow_id;
                        overTimeRequest.rq_status = 0;
                        overTimeRequest.rq_cre_by = getCurrentUser().u_id;
                        overTimeRequest.rq_cre_date = DateTime.Now;
                        ioverTimeRequest.Add(overTimeRequest);

                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        ViewBag.DepartmentList = (idepartment.GetDepartments);
                        return View();
                    }
                }
            }
            else
            {
                ViewBag.DepartmentList = (idepartment.GetDepartments);
                return View();
            }
        }

        // GET: OvertimeRequest/Edit/5
        public ActionResult Edit(int id, string from)
        {
            if (getCurrentUser() == null)
            {
                
                return RedirectToAction("Index", "Login");
            }
            else
            {
                
                ViewBag.from = from;
                ViewBag.DepartmentList = (idepartment.GetDepartments);
                return View(ioverTimeRequest.GetOverTimeRequest(id));
            }
        }

        // POST: OvertimeRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OverTimeRequest overTimeRequest, string from)
        {
            string from1 = from;
                if (getCurrentUser() == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            OverTimeRequest Requests = ioverTimeRequest.GetOverTimeRequest(id);
                            Requests.rq_description = overTimeRequest.rq_description;
                            Requests.rq_dep_id = overTimeRequest.rq_dep_id;
                            Requests.rq_start_time = overTimeRequest.rq_start_time;
                            Requests.rq_end_time = overTimeRequest.rq_end_time;
                            Requests.rq_no_of_hours = overTimeRequest.rq_no_of_hours;
                            Requests.rq_remarks = overTimeRequest.rq_remarks;
                            ioverTimeRequest.Update(Requests);
                            if (from1.Equals("Approval"))
                            {

                                return RedirectToAction("Approval");

                            }
                            else
                            {
                                return RedirectToAction(nameof(Index));
                            }

                        }
                        else
                        {
                        ViewBag.from = from;
                        ViewBag.DepartmentList = (idepartment.GetDepartments);
                        return View();
                        }
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                        ViewBag.from = from;
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
                
                return View(ioverTimeRequest.GetOverTimeRequest(id));
            }
        }

        // POST: OvertimeRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, OverTimeRequest overTimeRequest)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    ioverTimeRequest.Remove(id);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }
        [HttpPost]
        public ActionResult Initiate(int id, String from)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                Documents documents = idocuments.GetDocument(1);
                OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);
                WorkflowDetail workflow = iworkflowDetail.GetWorkFlowDetail(overTimeRequest.rq_workflow_id);
                int nextStatus = iworkflowDetail.getNextWorkflow(overTimeRequest.rq_workflow_id, overTimeRequest.rq_status);

                WorkflowDetail workflowDetail = iworkflowDetail.getWorkflowDetlByWorkflowCodeAndPriority(overTimeRequest.rq_workflow_id, nextStatus);
                WorkflowTracker workflowTracker = new WorkflowTracker();
                workflowTracker.wt_doc_id = documents.dc_id;
                workflowTracker.wt_fun_doc_id = overTimeRequest.rq_id;
                workflowTracker.wt_status = overTimeRequest.rq_status;
                workflowTracker.wt_workflow_id = overTimeRequest.rq_workflow_id;
                workflowTracker.wt_role_id = getCurrentUser().u_role_id;
                workflowTracker.wt_role_description = getCurrentUser().u_role_description;
                workflowTracker.wt_status_to = nextStatus;
                workflowTracker.wt_assign_role = workflowDetail.wd_role_id;
                workflowTracker.wt_assigned_role_name = workflowDetail.wd_role_description;
                workflowTracker.wt_approve_status = "Initiate";
                workflowTracker.wt_cre_by = getCurrentUser().u_id;
                workflowTracker.wt_cre_by_name = getCurrentUser().u_name;
                workflowTracker.wt_cre_date = DateTime.Now;
                iworkflowTracker.Add(workflowTracker);
                overTimeRequest.rq_status = nextStatus;
                ioverTimeRequest.Update(overTimeRequest);

                return RedirectToAction(from);
            }

        }
        [HttpPost]
        public ActionResult Approve(int id,String from)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                Documents documents =idocuments.GetDocument(1);
                OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);
                WorkflowDetail workflow = iworkflowDetail.GetWorkFlowDetail(overTimeRequest.rq_workflow_id);
                int nextStatus = iworkflowDetail.getNextWorkflow(overTimeRequest.rq_workflow_id, overTimeRequest.rq_status);
                int MinofWorkflow = iworkflowDetail.getMinOfWorkFlow(overTimeRequest.rq_workflow_id);
                int second = iworkflowDetail.getNextWorkflow(overTimeRequest.rq_workflow_id, 0);
                if (overTimeRequest.rq_status == second && getCurrentUser().u_dep_id != overTimeRequest.rq_dep_id)
                {
                    ViewBag.Message = "sdfhdsf";
                   
                    return RedirectToAction(from);
                }
                else
                {
                    WorkflowDetail workflowDetail = iworkflowDetail.getWorkflowDetlByWorkflowCodeAndPriority(overTimeRequest.rq_workflow_id, nextStatus);
                    WorkflowTracker workflowTracker = new WorkflowTracker();
                    workflowTracker.wt_doc_id = documents.dc_id;
                    workflowTracker.wt_fun_doc_id = overTimeRequest.rq_id;
                    workflowTracker.wt_status = overTimeRequest.rq_status;
                    workflowTracker.wt_workflow_id = overTimeRequest.rq_workflow_id;
                    workflowTracker.wt_role_id = getCurrentUser().u_role_id;
                    workflowTracker.wt_role_description = getCurrentUser().u_role_description;
                    workflowTracker.wt_status_to = nextStatus;
                    workflowTracker.wt_assign_role = workflowDetail.wd_role_id;
                    workflowTracker.wt_assigned_role_name = workflowDetail.wd_role_description;
                    workflowTracker.wt_approve_status = "Approved";
                    workflowTracker.wt_cre_by = getCurrentUser().u_id;
                    workflowTracker.wt_cre_by_name = getCurrentUser().u_name;
                    workflowTracker.wt_cre_date = DateTime.Now;
                    iworkflowTracker.Add(workflowTracker);
                    overTimeRequest.rq_status = nextStatus;
                    ioverTimeRequest.Update(overTimeRequest);
                    
                    return RedirectToAction(from);
                }
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
                
                return View(ioverTimeRequest.GetRequestForApprovals(getCurrentUser().u_id));
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
                Documents documents = idocuments.GetDocument(1);
                int previousStatus = iworkflowDetail.getPreviousWorkflow(overTimeRequest.rq_workflow_id, overTimeRequest.rq_status);
                WorkflowDetail workflowDetail = iworkflowDetail.getWorkflowDetlByWorkflowCodeAndPriority(overTimeRequest.rq_workflow_id, previousStatus);
                int second = iworkflowDetail.getNextWorkflow(overTimeRequest.rq_workflow_id, 0);
                if (overTimeRequest.rq_status == second && getCurrentUser().u_dep_id != overTimeRequest.rq_dep_id)
                {
                    return RedirectToAction("Approval");
                }
                else
                {
                    workflowTracker.wt_doc_id = documents.dc_id;
                    workflowTracker.wt_fun_doc_id = overTimeRequest.rq_id;
                    workflowTracker.wt_status = overTimeRequest.rq_status;
                    workflowTracker.wt_workflow_id = overTimeRequest.rq_workflow_id;
                    workflowTracker.wt_role_id = getCurrentUser().u_role_id;
                    workflowTracker.wt_role_description = getCurrentUser().u_role_description;
                    workflowTracker.wt_status_to = previousStatus;
                    workflowTracker.wt_assign_role = workflowDetail.wd_role_id;
                    workflowTracker.wt_assigned_role_name = workflowDetail.wd_role_description;
                    workflowTracker.wt_approve_status = "rejected";
                    workflowTracker.wt_cre_by = getCurrentUser().u_id;
                    workflowTracker.wt_cre_by_name = getCurrentUser().u_name;
                    workflowTracker.wt_cre_date = DateTime.Now;
                    iworkflowTracker.Add(workflowTracker);

                    overTimeRequest.rq_status = previousStatus;
                    ioverTimeRequest.Update(overTimeRequest);
                    return RedirectToAction("Approval");
                }
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
                ViewBag.RoleList = (irole.GetRoles);
                ViewBag.UserList = (iuser.GetUsersList());
                ViewBag.DepartmentList = (idepartment.GetDepartments);
                return View();
            }
        }
        [HttpPost]
        public ActionResult CustomReport(int rq_dep_id,DateTime rq_start_time,int no_of_hours, int role_id, int rq_cre_by,DateTime rq_cre_date,string approve)
        {
            System.Diagnostics.Debug.WriteLine(rq_dep_id+" "+ rq_start_time+" "+ no_of_hours+" "+ role_id+" "+ rq_cre_by+" "+ rq_cre_date+" "+ approve);
            return View(ioverTimeRequest.GetReports(rq_dep_id,rq_start_time, no_of_hours, role_id,rq_cre_by,rq_cre_date,approve));

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
                    return user;
                }

            }
            catch
            {
                return null;
            }
        }

        public ActionResult Consolidate()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.RoleList = (irole.GetRoles);
                ViewBag.UserList = (iuser.GetUsersList());
                ViewBag.DepartmentList = (idepartment.GetDepartments);
                return View();
            }
        }
        [HttpPost]
        public ActionResult ConsolidatedReports(int rq_dep_id, DateTime startDate, DateTime endDate,int rq_cre_by)
        {

            return View(ioverTimeRequest.getConsolidated(rq_dep_id, startDate, endDate,rq_cre_by));
        }

        [HttpPost]
        public IEnumerable<string> UsersName(string name)
        {

            return iuser.getUsersNames(name);
        }


        public ActionResult Start()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.DepartmentList = (idepartment.GetDepartments);
                ViewBag.MyLiveOvertimeRequest = ioverTimeRequest.GetMyLiveOvertimeRequest(getCurrentUser().u_id);
                ViewBag.MyOnProcessRequests = ioverTimeRequest.GetMyOnProcessRequests(getCurrentUser().u_id);
                return View();
                
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StartOverTime(OverTimeRequest overTimeRequest)
        {
           
                if (getCurrentUser() == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    try
                    {
                    IEnumerable<OverTimeRequest> active = ioverTimeRequest.GetMyLiveOvertimeRequest(getCurrentUser().u_id);

                    if (active.Count() == 0)
                    {

                        Documents documents = idocuments.GetDocument(1);
                        overTimeRequest.rq_doc_id = documents.dc_id;
                        overTimeRequest.rq_workflow_id = documents.dc_workflow_id;
                        overTimeRequest.rq_status = 0;
                        overTimeRequest.rq_start_time = DateTime.Now;
                        overTimeRequest.rq_end_time = null;
                        overTimeRequest.rq_cre_for = getCurrentUser().u_id;
                        overTimeRequest.rq_cre_by = getCurrentUser().u_id;
                        overTimeRequest.rq_cre_date = DateTime.Now;
                        ioverTimeRequest.Add(overTimeRequest);

                        return RedirectToAction(nameof(Start));
                        }
                        else
                        {
                            ViewBag.Message="You have Active OverTime Request!!";
                            return RedirectToAction(nameof(Start));
                        }
                    }
                    catch(Exception ex)
                    {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                        ViewBag.DepartmentList = (idepartment.GetDepartments);
                        return RedirectToAction(nameof(Start));
                    }
                }
            
        }
        [HttpPost]
        public ActionResult EndWork(int id, String from)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                Documents documents = idocuments.GetDocument(1);
                OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);
                WorkflowDetail workflow = iworkflowDetail.GetWorkFlowDetail(overTimeRequest.rq_workflow_id);
                int nextStatus = iworkflowDetail.getNextWorkflow(overTimeRequest.rq_workflow_id, overTimeRequest.rq_status);

                WorkflowDetail workflowDetail = iworkflowDetail.getWorkflowDetlByWorkflowCodeAndPriority(overTimeRequest.rq_workflow_id, nextStatus);
                WorkflowTracker workflowTracker = new WorkflowTracker();
                workflowTracker.wt_doc_id = documents.dc_id;
                workflowTracker.wt_fun_doc_id = overTimeRequest.rq_id;
                workflowTracker.wt_status = overTimeRequest.rq_status;
                workflowTracker.wt_workflow_id = overTimeRequest.rq_workflow_id;
                workflowTracker.wt_role_id = getCurrentUser().u_role_id;
                workflowTracker.wt_role_description = getCurrentUser().u_role_description;
                workflowTracker.wt_status_to = nextStatus;
                workflowTracker.wt_assign_role = workflowDetail.wd_role_id;
                workflowTracker.wt_assigned_role_name = workflowDetail.wd_role_description;
                workflowTracker.wt_approve_status = "WorkDone";
                workflowTracker.wt_cre_by = getCurrentUser().u_id;
                workflowTracker.wt_cre_by_name = getCurrentUser().u_name;
                workflowTracker.wt_cre_date = DateTime.Now;
                iworkflowTracker.Add(workflowTracker);
                overTimeRequest.rq_status = nextStatus;
                overTimeRequest.rq_end_time=DateTime.Now;
                ioverTimeRequest.Update(overTimeRequest);

                return RedirectToAction(nameof(Start));
            }

        }
        public ActionResult LiveMonitoring()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.CurrentDate = DateTime.Now;
                return View(ioverTimeRequest.GetAllLiveOvertimeRequest(getCurrentUser().u_id));
            }
        }

        [HttpPost]
        public ActionResult Hold(int id)
        {
           
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                 OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);

                /*overTimeRequest.rq_hold_yn = "Y";
                overTimeRequest.rq_hold_by = getCurrentUser().u_id;
                overTimeRequest.rq_hold_by_name= getCurrentUser().u_name;
                overTimeRequest.rq_hold_date = DateTime.Now;
                ioverTimeRequest.Update(overTimeRequest);*/
                return RedirectToAction(nameof(LiveMonitoring));

            }

        }

    }

}