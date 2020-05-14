using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class WorkflowDetail
    {
        [Key]
        public int wd_id { get; set; }
        [Display(Name = "workflow")]
        public int wd_workflow_id { get; set; }
        [Display(Name = "Role")]
        public int wd_role_id { get; set; }
        [Display(Name = "Priority")]
        public int wd_priority { get; set; }
        [Display(Name = "Active")]
        public string wd_active_yn { get; set; }
        [Display(Name = "Created by")]
        public int wd_cre_by { get; set; }
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime wd_cre_date { get; set; }
    }
}
