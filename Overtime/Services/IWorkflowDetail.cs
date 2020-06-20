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

        int getMinOfWorkFlow(int workflow);
        int getMaxOfWorkflow(int workflow);

        int getNextWorkflow(int workflow, int current);

        int getPreviousWorkflow(int workflow, int current);
        WorkflowDetail getWorkflowDetlByWorkflowCodeAndPriority(int wd_id, int priority);
        int getPriorityByRole(int rq_workflow_id, int r_id);
    }
}
