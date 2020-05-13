using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Repository
{
    public class OverTimeRequestRepository:IOverTimeRequest
    {
        private DBContext db;

        public OverTimeRequestRepository(DBContext _db)
        {
            db = _db;
        }

        public IEnumerable<OverTimeRequest> GetOvertimeRequests => db.OverTimeRequest;

        public IEnumerable<OverTimeRequest> getMyOvertimeRequests => db.OverTimeRequest.Where(s=> s.rq_status==0 &&s.rq_cre_by==12);

        public object getRequestForApprovals => 
            db.OverTimeRequest.Where(s => s.rq_status == 1 && s.rq_cre_by == 12);

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
    }
}
