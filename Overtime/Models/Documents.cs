using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class Documents
    {
        [Key]
        public int dc_id { get; set; }
        public string dc_description { get; set; }

        public int dc_workflow_id { get; set; }

        public string dc_active_yn { get; set; }

        public int dc_cre_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime dc_cre_date { get; set; }
    }
}
