using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class WorkflowTracker
    {
        [Key]
        public int wt_id { get; set; }

        public int wt_doc_id { get; set; }
        public int wt_workflow_id { get; set; }
        public int wt_role_id { get; set; }
        public int wt_assigned_to { get; set; }
        public string wf_aprove_status { get; set; }
        public int wt_cre_by { get; set; }
        [DataType(DataType.Date)]
        public DateTime wt_cre_date { get; set; }
    }
}
