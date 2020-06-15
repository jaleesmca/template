using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class Insight
    {
        [Key]
        public int in_id { get; set; }

       
        public int in_fun_doc_id { get; set; }
        public int in_doc_id { get; set; }

        [Display(Name = "insight")]
        public string in_remarks { get; set; }

        [Display(Name = "Cre by")]
        public int in_cre_by { get; set; }
        [DataType(DataType.Date)]

        [Display(Name = "Cre by Name")]
        public string in_cre_by_name { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Cre Date")]
        public DateTime in_cre_date { get; set; }
    }
    
}
