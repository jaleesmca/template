using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class WorkflowTrackerRepository : IWorkflowTracker
    {
        private DBContext db;

        public WorkflowTrackerRepository(DBContext _db)
        {
            db = _db;
        }
        public IEnumerable<WorkflowTracker> GetWorkflowTrackers => db.workflowTrackers;

        public void Add(WorkflowTracker workflowTracker)
        {
            db.workflowTrackers.Add(workflowTracker);
            db.SaveChanges();
        }

        public WorkflowTracker GetWorkflowTracker(int id)
        {
            WorkflowTracker workflowTracker = db.workflowTrackers.Find(id);
            return workflowTracker;
        }

        public IEnumerable<WorkflowTracker> GetWorkflowTrackersbyDocument(int rowid, int doc_id, int workflow)
        {
            var query = from u in db.workflowTrackers
                        where  u.wt_workflow_id == workflow && u.wt_doc_id == doc_id && u.wt_fun_doc_id == rowid
                        orderby u.wt_cre_date descending
                        select u;

            return query;
        }

        public void Remove(int id)
        {
            WorkflowTracker workflowTracker = db.workflowTrackers.Find(id);
            db.workflowTrackers.Remove(workflowTracker);
        }
    }
}
