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
    public class UserController : Controller
    {
        // GET: User
        private readonly IUser iuser;
        private readonly IRole irole;
        private readonly IDepartment idepartment;


        public UserController(IUser _iuser,IRole _irole, IDepartment _idepartment)
        {
            iuser=_iuser;
            irole = _irole;
            idepartment = _idepartment;
        }
        
        public ActionResult Index()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(iuser.GetUsersList());
            }
            
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.RoleList = (irole.GetRoles);
                ViewBag.DepartmentList = (idepartment.GetDepartments);
            }
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    user.u_cre_by = getCurrentUser().u_id;
                    user.u_cre_date = DateTime.Now;
                    user.u_active_yn = "Y";
                    iuser.Add(user);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.RoleList = (irole.GetRoles);
                ViewBag.DepartmentList = (idepartment.GetDepartments);
                return View(iuser.GetUser(id));
            }
        }
        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
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
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(iuser.GetUser(id));
            }
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,User user)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {

                    user = iuser.GetUser(id);
                    iuser.Remove(id);

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