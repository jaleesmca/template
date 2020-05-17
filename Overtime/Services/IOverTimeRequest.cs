﻿using Overtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Services
{
   public interface IOverTimeRequest
    {
        IEnumerable<OverTimeRequest> GetOvertimeRequests { get; }
        IEnumerable<OverTimeRequest> GetMyOvertimeRequests(int Currentuserid);
        IEnumerable<OverTimeRequest> GetRequestForApprovals(int id);

        IEnumerable<OverTimeRequest> GetReports(int rq_dep_id, DateTime rq_start_time, int rq_no_of_hours, int role_id, int rq_cre_by, DateTime rq_cre_date, string approve);
        OverTimeRequest GetOverTimeRequest(int id);

        void Add(OverTimeRequest overTimeRequest);

        void Remove(int id);

        void Approve(int id);
        void Update(OverTimeRequest overTimeRequest);
    }
}
