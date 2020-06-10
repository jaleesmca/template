using System;
using System.Collections.Generic;
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
        public AndroidController(IOverTimeRequest _ioverTimeReques, IWorkflowDetail _iworkflowDetail,
            IWorkflowTracker _iworkflowTracker, IDepartment _idepartment, IDocuments _idocuments, IRole _irole, IUser _iuser, IHold _ihold, IMenu _imenu)
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

        //[HttpPost]
        public String Approvals(int id)
        {
            //return "dsgfgfjhf";
            IEnumerable<OverTimeRequest> overTimeRequests= ioverTimeRequest.GetRequestForApprovals(11);
            string JsonStr = JsonConvert.SerializeObject(overTimeRequests);
            System.Diagnostics.Debug.WriteLine(JsonStr);
            return JsonStr;
        }
    }
}