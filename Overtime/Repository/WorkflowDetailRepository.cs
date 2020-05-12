using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class WorkflowDetailRepository : IWorkflowDetail
    {
        private DBContext db;

        public WorkflowDetailRepository(DBContext _db)
        {
            db = _db;
        }
        public IEnumerable<WorkflowDetail> GetWorkFlowDetails => db.WorkflowDetails;

        public void Add(WorkflowDetail workflowDetail)
        {
            db.WorkflowDetails.Add(workflowDetail);
            db.SaveChanges();
        }

        public WorkflowDetail GetWorkFlowDetail(int id)
        {
            WorkflowDetail workflowDetail = db.WorkflowDetails.Find(id);
            return workflowDetail;
        }

        public IEnumerable<WorkflowDetail> GetWorkFlowDetailsByWorkFlow(int Wf_id)
        {
            IEnumerable<WorkflowDetail> workflow = db.WorkflowDetails.Where(s => s.wd_workflow_id == Wf_id);
            
            Console.WriteLine(workflow);
            return workflow;
        }

        public void Remove(int id)
        {
            WorkflowDetail workflowDetail = db.WorkflowDetails.Find(id);
            db.WorkflowDetails.Remove(workflowDetail);
        }

    }
}
