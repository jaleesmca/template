using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class Menu
    {
        [Key]
        [Display(Name = "Id")]
        public int m_id { get; set; }

        [Display(Name = "Name")]
        public string m_description { get; set; }

        [Display(Name = "description to Show")]
        public string m_desc_to_show { get; set; }

        [Display(Name = "Link")]
        public string m_link { get; set; }

        [Display(Name = "Parrent")]
        public int? m_parrent_id { get; set; }

        [NotMapped]
        [Display(Name = "Parrent Name")]
        public string m_parrent_name { get; set; }

        [Display(Name = "Type")]
        public string m_type { get; set; }

        [Display(Name = "Active")]
        public string m_active_yn { get; set; }

        [Display(Name = "Created by id")]
        public int m_cre_by { get; set; }

        [NotMapped]
        [Display(Name = "Created by")]
        public string m_cre_by_Name { get; set; }

        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public DateTime m_cre_date { get; set; }

        [NotMapped]
        public IEnumerable<Menu> menuItem { get; set; }
    }
}
