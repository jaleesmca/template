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

        public int wt_fun_doc_id { get; set; }
        public int wt_doc_id { get; set; }
        public int wt_workflow_id { get; set; }
        [Display(Name = "Role id")]
        public int wt_role_id { get; set; }
        [Display(Name = "Role ")]
        public string wt_role_description { get; set; }
        public int wt_status { get; set; }
        public int wt_status_to { get; set; }
        [Display(Name = "Assigned ")]
        public int wt_assign_role { get; set; }
        [Display(Name = "Assigned To")]
        public string wt_assigned_role_name { get; set; }

        [Display(Name = "type")]
        public string wt_approve_status { get; set; }
        [Display(Name = "Cre by")]
        public int wt_cre_by { get; set; }
        [DataType(DataType.Date)]

        [Display(Name = "Cre by Name")]
        public string wt_cre_by_name { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Cre Date")]
        public DateTime wt_cre_date { get; set; }
    }
}
