using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class RoleMenu
    {
        [Key]
        [Display(Name = "Id")]
        public int rm_id { get; set; }

        [Display(Name = "role")]
        public int rm_role_id { get; set; }

        [Display(Name = "Menu id")]
        public int rm_menu_id { get; set; }

        [Display(Name = "Active")]
        public string rm_active_yn { get; set; }

        [NotMapped]
        [Display(Name = "Created by")]
        public string rm_cre_by_Name { get; set; }

        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public DateTime rm_cre_date { get; set; }
    }
}
