using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
   public interface IOverTimeRequest
    {
        IEnumerable<OverTimeRequest> GetOvertimeRequests { get; }
        IEnumerable<OverTimeRequest> getMyOvertimeRequests { get; }
        object getRequestForApprovals { get; }

        OverTimeRequest GetOverTimeRequest(int id);

        void Add(OverTimeRequest overTimeRequest);

        void Remove(int id);

        void Approve(int id);
        void Update(OverTimeRequest overTimeRequest);
    }
}
