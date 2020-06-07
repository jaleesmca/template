using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class UserDepartment
    {

        [Key]
        [Display(Name = "id")]
        public int ud_id { get; set; }

        [Display(Name = "User")]
        public int ud_user_id { get; set; }

        [NotMapped]
        [Display(Name = "User Name")]
        public string ud_user_name { get; set; }

        [Display(Name = "Department")]
        public int ud_depart_id { get; set; }

        [NotMapped]
        [Display(Name = "Department Desc")]
        public string ud_depart_desc { get; set; }

        public string ud_active_yn { get; set; }

        [Display(Name = "Created By")]
        public int ud_cre_by { get; set; }

        [NotMapped]
        [Display(Name = "Created By")]
        public string ud_cre_by_name { get; set; }

        [Display(Name = "Created Date")]

        [DataType(DataType.Date)]
        public DateTime ud_cre_date { get; set; }

    }
}
