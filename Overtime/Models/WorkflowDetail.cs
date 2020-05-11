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
        public int wd_workflow_id { get; set; }

        public int wd_role_id { get; set; }
        public int wd_priority { get; set; }

        public string wd_active_yn { get; set; }
        public int wd_cre_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime wd_cre_date { get; set; }
    }
}
