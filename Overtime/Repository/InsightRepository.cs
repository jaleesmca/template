using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class InsightRepository : IInsight
    {
        private DBContext db;

        public InsightRepository(DBContext _db)
        {
            db = _db;
        }
        public IEnumerable<Insight> GetInsights => db.Insights;

        public void Add(Insight insight)
        {
            db.Insights.Add(insight);
            db.SaveChanges();
        }

        public Insight GetInsight(int id)
        {
            Insight insight  = db.Insights.Find(id);
            return insight;
        }

        public void Remove(int id)
        {
            Insight insight = db.Insights.Find(id);
            db.Insights.Remove(insight);
            db.SaveChanges();
        }

        public void Update(Insight insight)
        {
            db.Insights.Update(insight);
            db.SaveChanges();
        }
        public IEnumerable<Insight> GetInsightsByDocument(int rowid, int doc_id)
        {
            var query = from u in db.Insights
                        join d in db.Users
                         on u.in_cre_by equals d.u_id
                        where u.in_doc_id == doc_id && u.in_fun_doc_id == rowid
                        orderby u.in_cre_date descending
                        select new Insight
                        {
                            in_id=u.in_id,
                            in_doc_id=u.in_doc_id,
                            in_fun_doc_id=u.in_fun_doc_id,
                            in_remarks=u.in_remarks,
                            in_cre_by=u.in_cre_by,
                            in_cre_by_name=d.u_name+'-'+d.u_full_name,
                            in_cre_date=u.in_cre_date

                        };
                            

            return query;
        }

    }
}
