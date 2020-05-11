using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class OverTimeRequest
    {
        [Key]
        public int rq_id { get; set; }

        public int rq_doc_id { get; set; }
        public string rq_description { get; set; }

        public int rq_dep_id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime rq_start_time { get; set; }

        public int rq_no_of_hours { get; set; }

        public string rq_remarks { get; set; }

        public int rq_status { get; set; }

        public string rq_active_yn { get; set; }

        public int rq_cre_by { get; set; }

        [DataType(DataType.Date)]
        public DateTime rq_cre_date { get; set; }
    }
}
