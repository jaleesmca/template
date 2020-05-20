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
    public class DepartmentController : Controller
    {
        private readonly IDepartment idepartment;
        public DepartmentController(IDepartment _idepartment)
        {
            idepartment = _idepartment;
        }
        // GET: Department
        public ActionResult Index()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(idepartment.GetDepartments);
            }
        }

        // GET: Department/Details/5
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

        // GET: Department/Create
        public ActionResult Create()
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

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    department.d_active_yn = "Y";
                    department.d_cre_by = 12;
                    department.d_cre_date = DateTime.Now;
                    idepartment.Add(department);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(idepartment.GetDepartment(id));
            }
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Department department)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    Department department1 = idepartment.GetDepartment(id);
                    department1.d_description = department.d_description;
                    idepartment.Update(department1);
                    // TODO: Add update logic here

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {


                return View(idepartment.GetDepartment(id));
            }
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Department department)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    idepartment.Remove(id);

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