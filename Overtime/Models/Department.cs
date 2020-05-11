using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class Department
    {
        [Key]
        public int d_id { get; set; }
        public string d_description { get; set; }

        public string d_active_yn { get; set; }

        public int d_cre_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime d_cre_date { get; set; }
    }
}
