using Microsoft.EntityFrameworkCore.Internal;
using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class OverTimeRequestRepository : IOverTimeRequest
    {
        private DBContext db;

        public OverTimeRequestRepository(DBContext _db)
        {
            db = _db;
        }

        public IEnumerable<OverTimeRequest> GetOvertimeRequests => db.OverTimeRequest;

        public IEnumerable<OverTimeRequest> GetMyOvertimeRequests(int Currentuserid)
        {
            
            return db.OverTimeRequest.Where(s => s.rq_status == 0 && s.rq_cre_by == Currentuserid);
        }


        public IEnumerable<OverTimeRequest> GetRequestForApprovals(int id) {
            var query = from r in db.OverTimeRequest
                        join w in db.WorkflowDetails
                          on r.rq_workflow_id equals w.wd_workflow_id
                        join m in db.Users
                          on w.wd_role_id equals m.u_role_id
                        where r.rq_status == w.wd_priority where m.u_id== id
                        select r;
            return query;
        }

        public void Add(OverTimeRequest overTimeRequest)
        {
            db.OverTimeRequest.Add(overTimeRequest);
            db.SaveChanges();
        }

        public void Approve(int id)
        {
          
        }

        public OverTimeRequest GetOverTimeRequest(int id)
        {
           OverTimeRequest overTimeRequest= db.OverTimeRequest.Find(id);
            return overTimeRequest;
        }

        public void Remove(int id)
        {
            OverTimeRequest overTimeRequest = db.OverTimeRequest.Find(id);
            db.OverTimeRequest.Remove(overTimeRequest);
            db.SaveChanges();
        }

        public void Update(OverTimeRequest overTimeRequest)
        {
            db.OverTimeRequest.Update(overTimeRequest);
            db.SaveChanges();

        }

        public IEnumerable<OverTimeRequest> GetReports(int rq_dep_id, DateTime rq_start_time, int no_of_hours, int rq_role_id, int rq_cre_by, DateTime rq_cre_date, string approve)
        {
            var query = from u in db.OverTimeRequest
                        join d in db.Departments
                          on u.rq_dep_id equals d.d_id
                        join k in db.Users
                         on u.rq_cre_by equals k.u_id
                        select new OverTimeRequest
                        {
                            rq_id = u.rq_id,
                            rq_workflow_id = u.rq_workflow_id,
                            rq_doc_id = u.rq_doc_id,
                            rq_description = u.rq_description,
                            rq_dep_id = u.rq_dep_id,
                            rq_dep_description=d.d_description,
                            rq_active_yn = u.rq_active_yn,
                            rq_start_time = u.rq_start_time,
                            rq_no_of_hours = u.rq_no_of_hours,
                            rq_status = u.rq_status,
                            rq_remarks=u.rq_remarks,
                            rq_cre_by = u.rq_cre_by,
                            rq_cre_date = u.rq_cre_date,
                        };
            if (rq_dep_id != 0)
                query = query.Where(x => x.rq_dep_id == rq_dep_id);
            if (no_of_hours != 0)
                query = query.Where(x => x.rq_no_of_hours == no_of_hours);
            if (!rq_start_time.ToString().Equals ("1/1/0001 12:00:00 AM"))
                query = query.Where(x => x.rq_start_time == rq_start_time);
            if (rq_cre_by != 0)
                query = query.Where(x => x.rq_cre_by == rq_cre_by);
            if (!rq_cre_date.ToString().Equals("1/1/0001 12:00:00 AM"))
                query = query.Where(x => x.rq_cre_date == rq_cre_date);
            return query;
        }
    }
}
