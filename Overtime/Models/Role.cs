using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class Role
    {
        [Key]
        [Display(Name = "Role Id")]
        public int r_id { get; set; }

        [Display(Name = "Role")]
        public string r_description { get; set;}

        [Display(Name = "Active")]
        public string r_active_yn { get; set; }
        [Display(Name = "Created by")]
        public int r_cre_by { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime r_cre_date { get; set; }
    }
}
