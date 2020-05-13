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
        public int u_id { get; set; }

        [Display(Name = "Username")]
        public string u_name
        {
            get; set;
        }

        [Display(Name = "Password")]
        public string u_password { get; set; }

        public int u_role_id { get; set; }

        [NotMapped]
        public string u_role_name { get; set; }

        public int u_dep_id { get; set; }

        public string u_active_yn { get; set; }

        public int u_cre_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime u_cre_date { get; set; }

    }
}
