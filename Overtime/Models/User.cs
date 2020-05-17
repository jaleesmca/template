using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class User
    {
        [Key]
        [Display(Name = "Id")]
        public int u_id { get; set; }

        [Display(Name = "Username")]
        public string u_name
        {
            get; set;
        }

        [Display(Name = "Password")]
        public string u_password { get; set; }

        [Display(Name = "Role Id")]
        public int u_role_id { get; set; }

        [NotMapped]
        [Display(Name = "Role Name")]
        public string u_role_description { get; set; }

        [Display(Name = "Dep Id")]
        public int u_dep_id { get; set; }

        [NotMapped]
        [Display(Name = "Dep Name")]
        public string u_dep_description { get; set; }

        [Display(Name = "Active")]
        public string u_active_yn { get; set; }

        [Display(Name = "Created")]
        public int u_cre_by { get; set; }

        [NotMapped]
        [Display(Name = "Created by")]
        public string u_cre_by_Name { get; set; }

        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public DateTime u_cre_date { get; set; }

    }
}
