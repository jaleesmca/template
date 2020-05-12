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
        OverTimeRequest GetOverTimeRequest(int id);

        void Add(OverTimeRequest overTimeRequest);

        void Remove(int id);
    }
}
