using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Overtime.Models;
using Overtime.Services;

namespace Overtime.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocuments idocuments;
        private readonly IWorkflow iworkflow;
        public DocumentsController(IDocuments _idocuments,IWorkflow _iworkflow)
        {
            idocuments = _idocuments;
            iworkflow = _iworkflow;
        }
        // GET: Documents
        public ActionResult Index()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(idocuments.GetDocumentsList());
            }
        }

        // GET: Documents/Details/5
        public ActionResult Details(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View();
            }
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            if (getCurrentUser() == null)
            {
              
                return RedirectToAction("Index", "Login");
            }
            else
            {

                ViewBag.WorkflowList = iworkflow.GetWorkflows;
                return View();
            }
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Documents documents)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    documents.dc_active_yn = "Y";
                    documents.dc_cre_by = 12;
                    documents.dc_cre_date = DateTime.Now;
                    idocuments.Add(documents);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.WorkflowList = iworkflow.GetWorkflows;
                return View(idocuments.GetDocument(id));
            }
        }

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Documents documents)
        {
            
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    Documents documents1 = idocuments.GetDocument(id);
                    documents1.dc_workflow_id = documents.dc_workflow_id;
                    documents1.dc_description = documents.dc_description;
                    idocuments.Update(documents1);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    ViewBag.WorkflowList = iworkflow.GetWorkflows;
                    return View();
                }
            }
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(idocuments.GetDocument(id));
            }
        }

        // POST: Documents/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    Documents document = idocuments.GetDocument(id);
                    idocuments.Remove(id);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }
        private User getCurrentUser()
        {
            try
            {
                if (HttpContext.Session.GetString("User") == null)
                {
                    return null;
                }
                else
                {
                    User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
                    ViewBag.Name = user.u_name;
                    ViewBag.isAdmin = user.u_is_admin;
                    return user;
                }

            }
            catch
            {
                return null;
            }
        }
    }
}