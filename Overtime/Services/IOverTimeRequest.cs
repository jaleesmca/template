using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
    interface IOverTimeRequest
    {
        IEnumerable<OverTimeRequest> GetRequests { get; }
        OverTimeRequest GetOverTimeRequest(int id);

        void Add(OverTimeRequest overTimeRequest);

        void Remove(int id);
    }
}
