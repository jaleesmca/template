using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public interface IWorkflowTracker
    {
        IEnumerable<WorkflowTracker> GetWorkflowTrackers { get; }
        WorkflowTracker GetWorkflowTracker(int id);

        void Add(WorkflowTracker workflowTracker);

        void Remove(int id);
        IEnumerable<WorkflowTracker> GetWorkflowTrackersbyDocument(int rowid, int doc_id, int workflow);
    }
}
