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

        public int getMaxOfWorkflow(int workflow)
        {
            int maxPriority = db.WorkflowDetails.Where(s => s.wd_workflow_id == workflow).Select(p => p.wd_priority).DefaultIfEmpty(0).Max();
            return maxPriority;
        }

        public int getMinOfWorkFlow(int workflow)
        {
            int minPriority = db.WorkflowDetails.Where(s => s.wd_workflow_id == workflow).
                Select(p => p.wd_priority).Min();
            return minPriority;
        }
        public int getNextWorkflow(int workflow, int current)
        {
            int next = db.WorkflowDetails.Where(s => s.wd_workflow_id == workflow &&s.wd_priority>current)
                .OrderBy(p => p.wd_priority).Select(p => p.wd_priority).FirstOrDefault();
            return next;
        }

        public int getPreviousWorkflow(int workflow, int current)
        {
            int previous = db.WorkflowDetails.Where(s => s.wd_workflow_id == workflow && s.wd_priority < current)
                .OrderByDescending(p => p.wd_priority).Select(p => p.wd_priority).FirstOrDefault();
            return previous;

        }
            public WorkflowDetail GetWorkFlowDetail(int id)
        {
            WorkflowDetail workflowDetail = db.WorkflowDetails.Find(id);
            return workflowDetail;
        }

        public IEnumerable<WorkflowDetail> GetWorkFlowDetailsByWorkFlow(int Wf_id)
        { var query = from wd in db.WorkflowDetails
                        join r in db.Roles
                          on wd.wd_role_id equals r.r_id
                        where wd.wd_workflow_id == Wf_id
                        orderby wd.wd_priority ,wd.wd_cre_date
                        select new WorkflowDetail
                        {
                            wd_id = wd.wd_id,
                            wd_workflow_id=wd.wd_workflow_id,
                            wd_role_id = wd.wd_role_id,
                            wd_role_description=r.r_description,
                            wd_priority=wd.wd_priority,
                            wd_cre_by=wd.wd_cre_by,
                            wd_active_yn=wd.wd_active_yn,
                            wd_cre_date=wd.wd_cre_date
                            
                        };

            return query;
        }

        public WorkflowDetail getWorkflowDetlByWorkflowCodeAndPriority(int wd_id, int priority)
        {
           var query= from wd in db.WorkflowDetails
            join r in db.Roles
              on wd.wd_role_id equals r.r_id
            where (wd.wd_workflow_id == wd_id && wd.wd_priority == priority) 
            orderby wd.wd_cre_date descending
            select new WorkflowDetail
            {
                wd_id = wd.wd_id,
                wd_workflow_id = wd.wd_workflow_id,
                wd_role_id = wd.wd_role_id,
                wd_role_description = r.r_description,
                wd_priority = wd.wd_priority,
                wd_cre_by = wd.wd_cre_by,
                wd_active_yn = wd.wd_active_yn,
                wd_cre_date = wd.wd_cre_date

            };
            return query.FirstOrDefault<WorkflowDetail>();
        }

        public void Remove(int id)
        {
            WorkflowDetail workflowDetail = db.WorkflowDetails.Find(id);
            db.WorkflowDetails.Remove(workflowDetail);
        }

    }
}
