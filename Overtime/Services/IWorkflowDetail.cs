using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    public interface IWorkflowDetail
    {
        IEnumerable<WorkflowDetail> GetWorkFlowDetails { get; }
        WorkflowDetail GetWorkFlowDetail(int id);

        void Add(WorkflowDetail workflow);

        void Remove(int id);

        IEnumerable<WorkflowDetail> GetWorkFlowDetailsByWorkFlow(int Workflow);
    }
}
