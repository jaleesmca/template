using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class MenuItems
    {
        public int m_id { get; set; }
        public string m_description { get; set; }
        public string m_desc_to_show { get; set; }
        public string m_link { get; set; }
        public int? m_parrent_id { get; set; }
        public string m_parrent_name { get; set; }
        public string m_type { get; set; }
        public string m_active_yn { get; set; }
        public int m_cre_by { get; set; }
        public string m_cre_by_Name { get; set; }
        public DateTime m_cre_date { get; set; }
        public IEnumerable<Menu> menuItem { get; set; }
    }
}
