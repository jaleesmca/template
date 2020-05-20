using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class Documents
    {
        [Key]
        [Display(Name = "Document No")]
        public int dc_id { get; set; }
        [Display(Name = "Document Name")]
        public string dc_description { get; set; }
        [Display(Name = "Workflow Code")]
        public int dc_workflow_id { get; set; }

        [NotMapped]
        [Display(Name = "Workflow Description")]
        public string doc_workflow_name { get; set; }

        public string dc_active_yn { get; set; }

        [Display(Name = "Created By")]
        public int dc_cre_by { get; set; }

        [Display(Name = "Created Date")]

        [DataType(DataType.Date)]
        public DateTime dc_cre_date { get; set; }
    }
}
