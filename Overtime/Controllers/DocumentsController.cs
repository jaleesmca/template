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
    public class DocumentsController : Controller
    {
        private readonly IDocuments idocuments;
        public DocumentsController(IDocuments _idocuments)
        {
            idocuments = _idocuments;
        }
        // GET: Documents
        public ActionResult Index()
        {

            return View(idocuments.GetDocuments);
        }

        // GET: Documents/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Documents/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Documents documents)
        {
            try
            {
                documents.dc_active_yn = "Y";
                documents.dc_cre_by=12;
                documents.dc_cre_date = DateTime.Now;
                idocuments.Add(documents);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int id)
        {
            return View(idocuments.GetDocument(id));
        }

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Documents documents)
        {
            try
            {
                idocuments.Update(documents);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int id)
        {
            return View(idocuments.GetDocument(id));
        }

        // POST: Documents/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Documents document=idocuments.GetDocument(id);
                idocuments.Remove(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}