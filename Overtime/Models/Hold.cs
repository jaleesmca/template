using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class Hold
    {
        [Key]
        [Display(Name = "Hold Id")]
        public int h_id { get; set; }

        
        [Display(Name = "Hold fun doc id")]
        public int h_fun_doc_id { get; set; }

       
        [Display(Name = "Hold doc Id")]
        public int h_doc_id { get; set; }


        [Display(Name = "Reason")]
        public string h_reasons { get; set; }

        [Display(Name = "Type")]
        public string h_type { get; set; }

        [Display(Name = "Replay")]
        public string h_replay { get; set; }

        [Display(Name = "Replay by")]
        public int h_replay_by { get; set; }

        [NotMapped]
        [Display(Name = "Replay By Name")]
        public string h_replay_by_name { get; set; }

        [Display(Name = "Replay Date")]
        public DateTime? h_replay_date { get; set; }

       
        [Display(Name = "Created by")]
        public int h_cre_by { get; set; }

        [NotMapped]
        [Display(Name = "Created by Name")]
        public string h_cre_by_name { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime h_cre_date { get; set; }
    }
}
