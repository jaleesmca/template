using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Overtime.Models;
using Overtime.Services;

namespace Overtime.Repository
{
    public class AndroidController : Controller
    {
        private readonly IUser iuser;
        private readonly IMenu imenu;
        private readonly IOverTimeRequest ioverTimeRequest;
        private readonly IWorkflowDetail iworkflowDetail;
        private readonly IWorkflowTracker iworkflowTracker;
        private readonly IDepartment idepartment;
        private readonly IDocuments idocuments;
        private readonly IRole irole;
        private readonly IHold ihold;
        private readonly IInsight iinsight;
        public AndroidController(IOverTimeRequest _ioverTimeReques, IWorkflowDetail _iworkflowDetail,
            IWorkflowTracker _iworkflowTracker, IDepartment _idepartment, IDocuments _idocuments, IRole _irole, IUser _iuser, IHold _ihold, IMenu _imenu,IInsight _iinsight)
        {
            ioverTimeRequest = _ioverTimeReques;
            iworkflowDetail = _iworkflowDetail;
            iworkflowTracker = _iworkflowTracker;
            idepartment = _idepartment;
            idocuments = _idocuments;
            irole = _irole;
            iuser = _iuser;
            ihold = _ihold;
            imenu = _imenu;
            iinsight = _iinsight;
        }
        [HttpPost]
        public String Login(string username,string password)
        {
            Result result = new Result();
           
            var key = "shdfg2323g3g4j3879sdfh2j3237w8eh";
           
            try
            {
                if (username != null && password != null)
                {


                    User newuser = iuser.getUserbyUsername(username);
                    if (newuser != null)
                    {

                        var newPassword = AesOperaions.DecryptString(key, newuser.u_password);

                        if (password.ToString().Equals(newPassword.ToString()))
                        {
                            newuser.u_password = null;
                            string JsonStr = JsonConvert.SerializeObject(newuser);
                            HttpContext.Session.SetString("User", JsonStr);
                            result.Objects = newuser;
                            result.Message = "Success";

                            return JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            result.Objects = null;
                            result.Message = "User Name and Password are incorrect!!!";

                            return JsonConvert.SerializeObject(result);
                        }
                    }
                    else
                    {
                        result.Objects = null;
                        result.Message = "User Name and Password are incorrect!!!";

                        return JsonConvert.SerializeObject(result);
                      
                    }
                }
                else
                {
                    result.Objects = null;
                    result.Message = "Please enter username and Password";

                    return JsonConvert.SerializeObject(result);
                }

            }
            catch (Exception ex)
            {
                result.Objects = null;
                result.Message = ex.Message;
                return JsonConvert.SerializeObject(result);
            }
        }

        [HttpPost]
        public String Approvals(int  u_id)
        {
            Result result = new Result();
            try
            {
                IEnumerable<OverTimeRequest> overTimeRequests = ioverTimeRequest.GetRequestForApprovals(u_id);
                string JsonStr = JsonConvert.SerializeObject(overTimeRequests);
                System.Diagnostics.Debug.WriteLine(JsonStr);
                result.Objects = overTimeRequests;
                result.Message = "Success";
                return JsonConvert.SerializeObject(result);
            }
            catch(Exception ex)
            {
                result.Objects = null;
                result.Message = ex.Message;
                return JsonConvert.SerializeObject(result);
            }
           
            
        }

        [HttpPost]
        public String LiveMonitoring(int u_id)
        {
           
            Result result = new Result();
            try
            {
               
                result.Objects = ioverTimeRequest.GetAllLiveOvertimeRequest(u_id);
                result.Message = "Success";
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                result.Objects = null;
                result.Message = ex.Message;
                return JsonConvert.SerializeObject(result);
            }

        }
        [HttpPost]
        public String HoldHistory(int rowid, int doc_id)
        {
            Result result = new Result();
            try
            {

                result.Objects = ihold.GetHoldsbyDocument(rowid, doc_id);
                result.Message = "Success";
                return JsonConvert.SerializeObject(result);
            }catch(Exception ex)
            {


                result.Objects = null;
                result.Message = ex.Message;
                return JsonConvert.SerializeObject(result);
            }
           
            
        }
        [HttpPost]
        public String Hold(int id, string reason,int u_id)
        {
           
            Result result = new Result();
            try {
                User user = iuser.GetUser(u_id);
                if (user != null)
                {
                    OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);
                    overTimeRequest.rq_hold_yn = "Y";
                    overTimeRequest.rq_hold_by = u_id;
                    overTimeRequest.rq_hold_by_name = user.u_name;
                    overTimeRequest.rq_hold_date = DateTime.Now;
                    ioverTimeRequest.Update(overTimeRequest);
                    Hold hold = new Hold();
                    hold.h_doc_id = overTimeRequest.rq_doc_id;
                    hold.h_fun_doc_id = overTimeRequest.rq_id;
                    hold.h_reasons = reason;
                    hold.h_type = "Hold";
                    hold.h_cre_by = user.u_id;
                    hold.h_cre_date = DateTime.Now;
                    ihold.Add(hold);
                    result.Objects = null;
                    result.Message = "Success";
                    return JsonConvert.SerializeObject(result);
                }else
                {
                    result.Objects = null;
                    result.Message = "You have No privilage to Hold";
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch(Exception ex)
            {


                result.Objects = null;
                result.Message = ex.Message;
                return JsonConvert.SerializeObject(result);
            }
}
        [HttpPost]
        public String AllHold(int u_id)
        {

            Result result = new Result();
            try
            {

                result.Objects = ioverTimeRequest.getAllHoldDocuments();
                result.Message = "Success";
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                result.Objects = null;
                result.Message = ex.Message;
                return JsonConvert.SerializeObject(result);
            }

        }
        [HttpPost]
        public String UnHold(int id, string reason, int u_id)
        {
            Result result = new Result();
            try
            {
                User user = iuser.GetUser(u_id);
                if (user != null)
                {
                    OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);
                    overTimeRequest.rq_hold_yn = null;
                    overTimeRequest.rq_hold_by = u_id;
                    overTimeRequest.rq_hold_by_name = user.u_name;
                    overTimeRequest.rq_hold_date = DateTime.Now;
                    ioverTimeRequest.Update(overTimeRequest);
                    Hold hold = new Hold();
                    hold.h_doc_id = overTimeRequest.rq_doc_id;
                    hold.h_fun_doc_id = overTimeRequest.rq_id;
                    hold.h_reasons = reason;
                    hold.h_type = "Unhold";
                    hold.h_cre_by = user.u_id;
                    hold.h_cre_date = DateTime.Now;
                    ihold.Add(hold);
                    result.Objects = null;
                    result.Message = "Success";
                    return JsonConvert.SerializeObject(result);
                }
                else
                {
                    result.Objects = null;
                    result.Message = "You have No privilage to unHold";
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch (Exception ex)
            {


                result.Objects = null;
                result.Message = ex.Message;
                return JsonConvert.SerializeObject(result);
            }
        }
        [HttpPost]
        public string Approve(int id, String reason,int u_id)
        {
            Result result = new Result();
            try
            {
                User user = iuser.GetUser(u_id);
                Role role = irole.GetRole(user.u_role_id);

                Documents documents = idocuments.GetDocument(1);
                OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);
                WorkflowDetail workflow = iworkflowDetail.GetWorkFlowDetail(overTimeRequest.rq_workflow_id);
                int nextStatus = iworkflowDetail.getNextWorkflow(overTimeRequest.rq_workflow_id, overTimeRequest.rq_status);
                int rolePriority = iworkflowDetail.getPriorityByRole(overTimeRequest.rq_workflow_id, role.r_id);
                int nextStatusbyUser = iworkflowDetail.getNextWorkflow(overTimeRequest.rq_workflow_id, rolePriority);
                int MinofWorkflow = iworkflowDetail.getMinOfWorkFlow(overTimeRequest.rq_workflow_id);
                if(nextStatus== nextStatusbyUser) {
                    if (overTimeRequest.rq_hold_yn == "Y")
                    {
                        result.Objects = null;
                        result.Message = "You have No privilage to unHold";
                        return JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        WorkflowDetail workflowDetail = iworkflowDetail.getWorkflowDetlByWorkflowCodeAndPriority(overTimeRequest.rq_workflow_id, nextStatus);
                        WorkflowTracker workflowTracker = new WorkflowTracker();
                        workflowTracker.wt_doc_id = documents.dc_id;
                        workflowTracker.wt_fun_doc_id = overTimeRequest.rq_id;
                        workflowTracker.wt_status = overTimeRequest.rq_status;
                        workflowTracker.wt_workflow_id = overTimeRequest.rq_workflow_id;
                        workflowTracker.wt_role_id = user.u_role_id;
                        workflowTracker.wt_role_description = role.r_description;
                        workflowTracker.wt_status_to = nextStatus;
                        workflowTracker.wt_assign_role = workflowDetail.wd_role_id;
                        workflowTracker.wt_assigned_role_name = workflowDetail.wd_role_description;
                        workflowTracker.wt_approve_status = "Approved";
                        workflowTracker.wt_cre_by = user.u_id;
                        workflowTracker.wt_cre_by_name = user.u_name + "-" + user.u_full_name;
                        workflowTracker.wt_cre_date = DateTime.Now;
                        iworkflowTracker.Add(workflowTracker);
                        overTimeRequest.rq_status = nextStatus;
                        ioverTimeRequest.Update(overTimeRequest);
                        if (!reason.Equals(""))
                        {
                            Insight insight = new Insight();
                            insight.in_fun_doc_id = overTimeRequest.rq_id;
                            insight.in_doc_id = overTimeRequest.rq_doc_id;
                            insight.in_remarks = reason;
                            insight.in_cre_by = user.u_id;
                            insight.in_cre_date = DateTime.Now;

                            iinsight.Add(insight);
                        }
                        result.Objects = null;
                        result.Message = "Success";
                        return JsonConvert.SerializeObject(result);
                    }

                }
                else
                {
                    result.Objects = null;
                    result.Message = "You have No privilage to Approve";
                    return JsonConvert.SerializeObject(result);
                }
               
            }
            catch(Exception ex)
            {
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                result.Objects = null;
                result.Message = ex.Message+""+ line;

                return JsonConvert.SerializeObject(result);
            }
        }
        [HttpPost]
        public string Reject(int id, String reason, int u_id)
        {


            Result result = new Result();
            try
            {
                if (reason != null)
                {
                    User user = iuser.GetUser(u_id);
                    Role role = irole.GetRole(user.u_role_id);

                    OverTimeRequest overTimeRequest = ioverTimeRequest.GetOverTimeRequest(id);
                    WorkflowDetail workflow = iworkflowDetail.GetWorkFlowDetail(overTimeRequest.rq_workflow_id);
                    WorkflowTracker workflowTracker = new WorkflowTracker();
                    Documents documents = idocuments.GetDocument(1);
                    int previousStatus = iworkflowDetail.getPreviousWorkflow(overTimeRequest.rq_workflow_id, overTimeRequest.rq_status);
                    int rolePriority = iworkflowDetail.getPriorityByRole(overTimeRequest.rq_workflow_id, role.r_id);
                    int previousStatusByRole = iworkflowDetail.getPreviousWorkflow(overTimeRequest.rq_workflow_id, rolePriority);
                    if (previousStatus == previousStatusByRole)
                    {
                        WorkflowDetail workflowDetail = iworkflowDetail.getWorkflowDetlByWorkflowCodeAndPriority(overTimeRequest.rq_workflow_id, previousStatus);


                        workflowTracker.wt_doc_id = documents.dc_id;
                        workflowTracker.wt_fun_doc_id = overTimeRequest.rq_id;
                        workflowTracker.wt_status = overTimeRequest.rq_status;
                        workflowTracker.wt_workflow_id = overTimeRequest.rq_workflow_id;
                        workflowTracker.wt_role_id = user.u_role_id;
                        workflowTracker.wt_role_description = role.r_description;
                        workflowTracker.wt_status_to = previousStatus;
                        workflowTracker.wt_assign_role = workflowDetail.wd_role_id;
                        workflowTracker.wt_assigned_role_name = workflowDetail.wd_role_description;
                        workflowTracker.wt_approve_status = "rejected";
                        workflowTracker.wt_cre_by = user.u_id;
                        workflowTracker.wt_cre_by_name = user.u_name + "-" + user.u_full_name;
                        workflowTracker.wt_cre_date = DateTime.Now;
                        iworkflowTracker.Add(workflowTracker);
                        overTimeRequest.rq_status = previousStatus;
                        ioverTimeRequest.Update(overTimeRequest);
                        if (!reason.Equals(""))
                        {
                            Insight insight = new Insight();
                            insight.in_fun_doc_id = overTimeRequest.rq_id;
                            insight.in_doc_id = overTimeRequest.rq_doc_id;
                            insight.in_remarks = reason;
                            insight.in_cre_by = user.u_id;
                            insight.in_cre_date = DateTime.Now;

                            iinsight.Add(insight);
                        }

                        result.Objects = null;
                        result.Message = "Success";
                        return JsonConvert.SerializeObject(result);

                    }
                    else
                    {
                        result.Objects = null;
                        result.Message = "You have no priviage";
                        return JsonConvert.SerializeObject(result);
                    }
                }else
                {
                    result.Objects = null;
                    result.Message = "Please enter Remarks";
                    return JsonConvert.SerializeObject(result);
                }
            }

            catch (Exception ex)
            {
              /*  var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();*/
                result.Objects = null;
                result.Message = ex.Message;

                return JsonConvert.SerializeObject(result);
            }
        }
        [HttpPost]
        public String ApprovalHistory(int rowid, int doc_id, int workflow)
        {
            Result result = new Result();
            try
            {
                result.Objects = iworkflowTracker.GetWorkflowTrackersbyDocument(rowid, doc_id, workflow);
                result.Message = "Success";


                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                result.Objects = iworkflowTracker.GetWorkflowTrackersbyDocument(rowid, doc_id, workflow);
                result.Message = ex.Message ;


                return JsonConvert.SerializeObject(result);
            }

            
        }
    }
}