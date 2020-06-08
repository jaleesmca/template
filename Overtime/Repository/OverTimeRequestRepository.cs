using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NHibernate.Util;
using Overtime.Models;
using Overtime.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
            var query = from u in db.OverTimeRequest
                        join d in db.Departments
                        on u.rq_dep_id equals d.d_id
                        join j in db.Users
                        on u.rq_cre_by equals j.u_id
                        join x in db.Users
                        on u.rq_cre_for equals x.u_id
                        where u.rq_status == 0
                        where u.rq_cre_by== Currentuserid
                        select new OverTimeRequest
                        {
                            rq_id = u.rq_id,
                            rq_cre_for = u.rq_cre_for,
                            rq_cre_for_name = x.u_full_name,
                            rq_cre_for_emp_id = x.u_name,
                            rq_workflow_id = u.rq_workflow_id,
                            rq_doc_id = u.rq_doc_id,
                            rq_description = u.rq_description,
                            rq_dep_id = u.rq_dep_id,
                            rq_dep_description = d.d_description,
                            rq_end_time=u.rq_end_time,
                            rq_active_yn = u.rq_active_yn,
                            rq_start_time = u.rq_start_time,
                            rq_no_of_hours = u.rq_no_of_hours,
                            rq_status = u.rq_status,
                            rq_remarks = u.rq_remarks,
                            rq_hold_yn = u.rq_hold_yn,
                            rq_hold_date = u.rq_hold_date,
                            rq_hold_by = u.rq_hold_by,
                            rq_cre_by = u.rq_cre_by,
                            rq_cre_by_name = j.u_full_name,
                            rq_cre_date = u.rq_cre_date,
                        };
            return query;
        }


        public IEnumerable<OverTimeRequest> GetRequestForApprovals(int id)
        {
            var qry= from u in db.Users
                        where u.u_id == id
                        select u;
            User currentUser = qry.FirstOrDefault();

            int workflow = (from u in db.Documents
                            where u.dc_id == 1
                            select u.dc_workflow_id).FirstOrDefault();
           
            var  query = from u in db.OverTimeRequest
                        join w in db.WorkflowDetails
                          on u.rq_workflow_id equals w.wd_workflow_id
                        join d in db.Departments
                        on u.rq_dep_id equals d.d_id
                        join ud in db.UserDepartments on u.rq_dep_id  equals ud.ud_depart_id 
                        join m in db.Users on
                         w.wd_role_id equals m.u_role_id
                        join j in db.Users
                        on u.rq_cre_by equals j.u_id
                        join x in db.Users
                        on u.rq_cre_for equals x.u_id
                        where u.rq_status == w.wd_priority
                        where m.u_id == id
                        where ud.ud_user_id==m.u_id
                         select new OverTimeRequest
                        {
                            rq_id = u.rq_id,
                            rq_cre_for=u.rq_cre_for,
                            rq_cre_for_name=x.u_full_name,
                            rq_cre_for_emp_id=x.u_name,
                            rq_workflow_id = u.rq_workflow_id,
                            rq_doc_id = u.rq_doc_id,
                            rq_description = u.rq_description,
                            rq_dep_id = u.rq_dep_id,
                            rq_end_time = u.rq_end_time,
                            rq_dep_description = d.d_description,
                            rq_active_yn = u.rq_active_yn,
                            rq_start_time = u.rq_start_time,
                            rq_no_of_hours = u.rq_no_of_hours,
                            rq_status = u.rq_status,
                            rq_hold_yn = u.rq_hold_yn,
                            rq_hold_date = u.rq_hold_date,
                            rq_hold_by = u.rq_hold_by,
                            rq_remarks = u.rq_remarks,
                            rq_cre_by = u.rq_cre_by,
                            rq_cre_by_name = j.u_full_name,
                            rq_cre_date = u.rq_cre_date,
                        };

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
           
            OverTimeRequest overTimeRequest = db.OverTimeRequest.Find(id);
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

        public IEnumerable<OverTimeRequest> GetReports(int rq_dep_id, DateTime rq_start_time, DateTime rq_end_time, int no_of_hours, int rq_role_id, int rq_cre_by, DateTime rq_cre_date, string approve)
        {
            var query = from u in db.OverTimeRequest
                        join d in db.Departments
                          on u.rq_dep_id equals d.d_id
                        join k in db.Users
                         on u.rq_cre_by equals k.u_id
                        join x in db.Users
                        on u.rq_cre_for equals x.u_id
                        orderby u.rq_id descending
                        select new OverTimeRequest
                        {
                            rq_id = u.rq_id,
                            rq_cre_for = u.rq_cre_for,
                            rq_cre_for_name = x.u_full_name,
                            rq_cre_for_emp_id = x.u_name,
                            rq_workflow_id = u.rq_workflow_id,
                            rq_doc_id = u.rq_doc_id,
                            rq_description = u.rq_description,
                            rq_dep_id = u.rq_dep_id,
                            rq_dep_description = d.d_description,
                            rq_active_yn = u.rq_active_yn,
                            rq_end_time = u.rq_end_time,
                            rq_start_time = u.rq_start_time,
                            rq_no_of_hours = u.rq_no_of_hours,
                            rq_status = u.rq_status,
                            rq_hold_yn = u.rq_hold_yn,
                            rq_hold_date = u.rq_hold_date,
                            rq_hold_by = u.rq_hold_by,
                            rq_remarks = u.rq_remarks,
                            rq_cre_by = u.rq_cre_by,
                            rq_cre_by_name = k.u_full_name,
                            rq_cre_date = u.rq_cre_date,
                        };
            if (rq_dep_id != 0)
                query = query.Where(x => x.rq_dep_id == rq_dep_id);
            if (no_of_hours != 0)
                query = query.Where(x => x.rq_no_of_hours == no_of_hours);
            if (!rq_start_time.ToString().Equals("1/1/0001 12:00:00 AM") && !rq_end_time.ToString().Equals("1/1/0001 12:00:00 AM"))
                query = query.Where(x => x.rq_start_time >= rq_start_time && rq_start_time<=rq_end_time );
            if (rq_cre_by != 0)
                query = query.Where(x => x.rq_cre_by == rq_cre_by);
            if (!rq_cre_date.ToString().Equals("1/1/0001 12:00:00 AM"))
                query = query.Where(x => x.rq_cre_date == rq_cre_date);
            return query;
        }

        public IEnumerable<OvCustomModel> getConsolidatedAsync(int rq_dep_id, DateTime startDate, DateTime endDate, int rq_cre_for,string type)
        {
            List<OvCustomModel> result = new List<OvCustomModel>();
            var conn = db.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = @"select rq_cre_for,u_name,u_full_name,rq_dep_id,d_description,
                        sum([dbo].[fn_GetTotalWorkingHours](rq_start_time, rq_end_time)) hours
                        from OverTimeRequest
                        join Users on rq_cre_for = u_id
                        join Departments on rq_dep_id = d_id
                        where 1=1 ";

                    if(type.Equals("Approve")) query=query+" and [dbo].[get_workflow_min_max](rq_workflow_id, 'max') = rq_status";
                    if(type.Equals("Unapprove")) query = query + " and [dbo].[get_workflow_min_max](rq_workflow_id, 'max') != rq_status";

                    if (!startDate.ToString().Equals("1/1/0001 12:00:00 AM")&& !endDate.ToString().Equals("1/1/0001 12:00:00 AM"))
                        query +=" and rq_start_time between '"+ startDate + @"' and '"+ endDate + @"' ";
                    if (rq_dep_id != 0) query += " and rq_dep_id='"+ rq_dep_id + @"'";
                    if (rq_cre_for !=0) query += " and rq_cre_for='"+ rq_cre_for + @"'";

                    query += " group by rq_cre_for,u_name,u_full_name,rq_dep_id,d_description";
                    System.Diagnostics.Debug.WriteLine(query);
                    command.CommandText = query;
                    DbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            OvCustomModel ovCustomModel = new OvCustomModel();
                            ovCustomModel.emp_id = reader.GetInt32(0);
                            ovCustomModel.emp_name = reader.GetString(1);
                            ovCustomModel.emp_full_name = reader.GetString(2);
                            ovCustomModel.emp_dep_id = reader.GetInt32(3);
                            ovCustomModel.emp_dep_name = reader.GetString(4);
                            ovCustomModel.work_hours = reader.GetDecimal(5);
                            result.Add(ovCustomModel);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public dynamic GetMyOnProcessRequests(int u_id)
        {
            var query = from u in db.OverTimeRequest
                        join d in db.Departments
                        on u.rq_dep_id equals d.d_id
                        join j in db.Users
                        on u.rq_cre_by equals j.u_id
                        join x in db.Users
                        on u.rq_cre_for equals x.u_id
                        where u.rq_status !=0
                        where u.rq_cre_by == u_id
                        select new OverTimeRequest
                        {
                            rq_id = u.rq_id,
                            rq_cre_for = u.rq_cre_for,
                            rq_cre_for_name = x.u_full_name,
                            rq_cre_for_emp_id = x.u_name,
                            rq_workflow_id = u.rq_workflow_id,
                            rq_doc_id = u.rq_doc_id,
                            rq_description = u.rq_description,
                            rq_dep_id = u.rq_dep_id,
                            rq_dep_description = d.d_description,
                            rq_active_yn = u.rq_active_yn,
                            rq_start_time = u.rq_start_time,
                            rq_no_of_hours = u.rq_no_of_hours,
                            rq_status = u.rq_status,
                            rq_end_time = u.rq_end_time,
                            rq_remarks = u.rq_remarks,
                            rq_cre_by = u.rq_cre_by,
                            rq_hold_yn = u.rq_hold_yn,
                            rq_hold_date = u.rq_hold_date,
                            rq_hold_by = u.rq_hold_by,
                            rq_cre_by_name = j.u_full_name,
                            rq_cre_date = u.rq_cre_date,
                        };
            return query;
        }

        public IEnumerable<OverTimeRequest> GetMyLiveOvertimeRequest(int u_id)
        {
            var query = from u in db.OverTimeRequest
                        join d in db.Departments
                        on u.rq_dep_id equals d.d_id
                        join j in db.Users
                        on u.rq_cre_by equals j.u_id
                        join x in db.Users
                        on u.rq_cre_for equals x.u_id
                        where u.rq_status == 0
                        where u.rq_cre_by == u_id
                        where u.rq_end_time == null
                        select new OverTimeRequest
                        {
                            rq_id = u.rq_id,
                            rq_cre_for = u.rq_cre_for,
                            rq_cre_for_name = x.u_full_name,
                            rq_cre_for_emp_id = x.u_name,
                            rq_workflow_id = u.rq_workflow_id,
                            rq_doc_id = u.rq_doc_id,
                            rq_description = u.rq_description,
                            rq_dep_id = u.rq_dep_id,
                            rq_dep_description = d.d_description,
                            rq_active_yn = u.rq_active_yn,
                            rq_start_time = u.rq_start_time,
                            rq_no_of_hours = u.rq_no_of_hours,
                            rq_status = u.rq_status,
                            rq_end_time = u.rq_end_time,
                            rq_remarks = u.rq_remarks,
                            rq_hold_yn = u.rq_hold_yn,
                            rq_hold_date = u.rq_hold_date,
                            rq_hold_by = u.rq_hold_by,
                            rq_cre_by = u.rq_cre_by,
                            rq_cre_by_name = j.u_full_name,
                            rq_cre_date = u.rq_cre_date,
                        };
            return query;
        }

        public object GetAllLiveOvertimeRequest(int u_id)
        {
            var query = from u in db.OverTimeRequest
                        join d in db.Departments
                        on u.rq_dep_id equals d.d_id
                        join j in db.Users
                        on u.rq_cre_by equals j.u_id
                        join x in db.Users
                        on u.rq_cre_for equals x.u_id
                        where u.rq_status == 0
                        where u.rq_end_time == null
                        select new OverTimeRequest
                        {
                            rq_id = u.rq_id,
                            rq_cre_for = u.rq_cre_for,
                            rq_cre_for_name = x.u_full_name,
                            rq_cre_for_emp_id = x.u_name,
                            rq_workflow_id = u.rq_workflow_id,
                            rq_doc_id = u.rq_doc_id,
                            rq_description = u.rq_description,
                            rq_dep_id = u.rq_dep_id,
                            rq_dep_description = d.d_description,
                            rq_active_yn = u.rq_active_yn,
                            rq_start_time = u.rq_start_time,
                            rq_no_of_hours = u.rq_no_of_hours,
                            rq_status = u.rq_status,
                            rq_end_time = u.rq_end_time,
                            rq_remarks = u.rq_remarks,
                            rq_hold_yn=u.rq_hold_yn,
                            rq_hold_date=u.rq_hold_date,
                            rq_hold_by=u.rq_hold_by,
                            rq_cre_by = u.rq_cre_by,
                            rq_cre_by_name = j.u_full_name,
                            rq_cre_date = u.rq_cre_date,
                        };
            return query;
        }

        public IEnumerable<OverTimeRequest> getAllHoldDocuments()
        {
            var query = from u in db.OverTimeRequest
                        join d in db.Departments
                        on u.rq_dep_id equals d.d_id
                        join j in db.Users
                        on u.rq_cre_by equals j.u_id
                        join x in db.Users
                        on u.rq_cre_for equals x.u_id
                        where u.rq_status != 0
                        where u.rq_hold_yn=="Y"
                        select new OverTimeRequest
                        {
                            rq_id = u.rq_id,
                            rq_cre_for = u.rq_cre_for,
                            rq_cre_for_name = x.u_full_name,
                            rq_cre_for_emp_id = x.u_name,
                            rq_workflow_id = u.rq_workflow_id,
                            rq_doc_id = u.rq_doc_id,
                            rq_description = u.rq_description,
                            rq_dep_id = u.rq_dep_id,
                            rq_dep_description = d.d_description,
                            rq_active_yn = u.rq_active_yn,
                            rq_start_time = u.rq_start_time,
                            rq_no_of_hours = u.rq_no_of_hours,
                            rq_status = u.rq_status,
                            rq_end_time = u.rq_end_time,
                            rq_remarks = u.rq_remarks,
                            rq_hold_yn = u.rq_hold_yn,
                            rq_hold_date = u.rq_hold_date,
                            rq_hold_by = u.rq_hold_by,
                            rq_cre_by = u.rq_cre_by,
                            rq_cre_by_name = j.u_full_name,
                            rq_cre_date = u.rq_cre_date,
                        };
            return query;
        }
    }
}
