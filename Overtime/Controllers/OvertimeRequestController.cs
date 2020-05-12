using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overtime.Models;
using Overtime.Services;

namespace Overtime.Controllers
{
    public class OvertimeRequestController : Controller
    {
        private readonly IOverTimeRequest ioverTimeRequest;
       
        public OvertimeRequestController(IOverTimeRequest _ioverTimeReques)
        {
            ioverTimeRequest = _ioverTimeReques;
        }

        // GET: OvertimeRequest
        public ActionResult Index()
        {
            return View(ioverTimeRequest.GetOvertimeRequests);
        }

        // GET: OvertimeRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OvertimeRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OvertimeRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OverTimeRequest overTimeRequest)
        {
            try
            {
                ioverTimeRequest.Add(overTimeRequest);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OvertimeRequest/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View(ioverTimeRequest.GetOverTimeRequest(id));
        }

        // POST: OvertimeRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OvertimeRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OvertimeRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}