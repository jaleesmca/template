using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class Workflow
    {

        [Key]
        public int w_id { get; set; }

        [Display(Name = "Workflow name")]
        public string w_description { get; set; }

        [Display(Name = "Active")]
        public string w_active_yn { get; set; }
        [Display(Name = "Created by")]
        public int w_cre_by { get; set; }
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime w_cre_date { get; set; }
    }
}
