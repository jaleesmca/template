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
        public int r_id { get; set; }
        public string r_description { get; set;}

        public string r_active_yn { get; set; }

        public int r_cre_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime r_cre_date { get; set; }
    }
}
