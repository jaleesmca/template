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
    public class UserController : Controller
    {
        // GET: User
        private readonly IUser iuser;
        private readonly IRole irole;

        public UserController(IUser _iuser,IRole _irole)
        {
            iuser=_iuser;
            irole = _irole;
        }
        public ActionResult Index()
        {
           
            return View(iuser.GetUsers);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.RoleList = (irole.GetRoles);
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                iuser.Add(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {

            return View(iuser.GetUser(id));
        }
        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View(iuser.GetUser(id));
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,User user)
        {
            try
            {
                User user1= iuser.GetUser(id);
                iuser.Remove(id);
            
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                return View();
            }
        }
    }
}