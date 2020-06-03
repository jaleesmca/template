using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class HoldRepository : IHold
    {
        private readonly DBContext db;

        public HoldRepository(DBContext _db)
        {
            db = _db;
        }

        public IEnumerable<Hold> GetHolds => db.Holds;

        public void Add(Hold hold)
        {
            db.Holds.Add(hold);
            db.SaveChanges();
        }

        public Hold GetHold(int id)
        {
            Hold hold = db.Holds.Find(id);
            return hold;
        }

        public void Remove(int id)
        {
            Hold hold = db.Holds.Find(id);
            db.Holds.Remove(hold);
            db.SaveChanges();
        }

        public void Update(Hold hold)
        {
            db.Update(hold);
            db.SaveChanges();
        }
        public IEnumerable<Hold> GetHoldsbyDocument(int rowid, int doc_id)
        {

            var query = from h in db.Holds
                        join u in db.Users
                         on h.h_cre_by equals u.u_id
                        join u2 in db.Users
                        on h.h_replay_by equals u2.u_id  into j2
                        from replayBy in j2.DefaultIfEmpty()
                        where h.h_doc_id== doc_id&& h.h_fun_doc_id==rowid
                        orderby h.h_cre_date descending
                        select new Hold
                        {
                            h_id = h.h_id,
                            h_doc_id=h.h_doc_id,
                            h_fun_doc_id=h.h_fun_doc_id,
                            h_reasons=h.h_reasons,
                            h_replay=h.h_replay,
                            h_replay_by=h.h_replay_by,
                            h_replay_by_name = replayBy.u_full_name ?? string.Empty,
                            h_replay_date=h.h_replay_date,
                            h_cre_by_name =u.u_full_name,
                            h_type=h.h_type,
                            h_cre_by = h.h_cre_by,
                            h_cre_date = h.h_cre_date
                        };
           
            return query;
        }
    }
}
