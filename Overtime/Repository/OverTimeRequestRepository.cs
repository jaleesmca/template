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

        public IEnumerable<OverTimeRequest> GetRequests => db.OverTimeRequests;

        public void Add(OverTimeRequest overTimeRequest)
        {
            db.OverTimeRequests.Add(overTimeRequest);
        }

        public OverTimeRequest GetOverTimeRequest(int id)
        {
           OverTimeRequest overTimeRequest= db.OverTimeRequests.Find(id);
            return overTimeRequest;
        }

        public void Remove(int id)
        {
            OverTimeRequest overTimeRequest = db.OverTimeRequests.Find(id);
            db.OverTimeRequests.Remove(overTimeRequest);
        }
    }
}
