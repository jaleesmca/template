using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class OvCustomModel
    {
        
        public int id { get; set; }

        public int emp_id { get; set; }

        [Display(Name = "Employee Code")]
        public string emp_name { get; set; }

        [Display(Name = "Employee Name")]
        public string emp_full_name { get; set; }

        [Display(Name = "Department id")]
        public int emp_dep_id { get; set; }

        [Display(Name = "Department")]
        public string emp_dep_name { get; set; }

        [Display(Name = "Approved Hours")]
        public decimal work_hours { get; set; }

        [Display(Name = "Unapproved Hours")]
        public decimal emp_unapproved { get; set; }

        [Display(Name = "Total Hours")]
        public decimal emp_total { get; set; }

    }
}
