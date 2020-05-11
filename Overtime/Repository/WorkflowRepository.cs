using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class WorkflowRepository : IWorkflow
    {
        private DBContext db;

        public WorkflowRepository(DBContext _db)
        {
            db = _db;
        }
        public IEnumerable<Workflow> GetWorkflows { get => throw new NotImplementedException(); }

        public void Add(Workflow workflow)
        {
            db.Workflows.Add(workflow);
            db.SaveChanges();
        }

        public Workflow GetWorkflow(int id)
        {
            Workflow workflow = db.Workflows.Find(id);
            return workflow;
        }

        public void Remove(int id)
        {
            Workflow workflow = db.Workflows.Find(id);
            db.Workflows.Remove(workflow);
        }
    }
}
