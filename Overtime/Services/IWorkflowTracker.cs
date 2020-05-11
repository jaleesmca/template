using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    interface IWorkflowTracker
    {
        IEnumerable<WorkflowTracker> GetWorkflowTrackers { get; }
        WorkflowTracker GetWorkflowTracker(int id);

        void Add(WorkflowTracker workflowTracker);

        void Remove(int id);
    }
}
