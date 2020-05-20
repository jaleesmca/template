using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Overtime.Models;
using Overtime.Repository;
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
                    if (user.u_dep_id != 0 && user.u_role_id != 0)
                    {
                        User usercheck = iuser.getUserbyUsername(user.u_name);
                        if (usercheck == null)
                        {
                            var key = "shdfg2323g3g4j3879sdfh2j3237w8eh";
                            var encryptedString = AesOperaions.EncryptString(key, user.u_password);
                            user.u_password = encryptedString.ToString();
                            user.u_cre_by = getCurrentUser().u_id;
                            user.u_cre_date = DateTime.Now;
                            user.u_active_yn = "Y";
                            iuser.Add(user);
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ViewBag.RoleList = (irole.GetRoles);
                            ViewBag.DepartmentList = (idepartment.GetDepartments);
                            ViewBag.Message = "Username already exsist";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Please enter all column";
                        return View();
                    }

                    
                }
                catch
                {
                    ViewBag.RoleList = (irole.GetRoles);
                    ViewBag.DepartmentList = (idepartment.GetDepartments);
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
                User user = iuser.GetUser(id);
                user.u_password = null;
                return View(user);
            }
        }
        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    User temp_user = iuser.GetUser(id);
                    
                    var key = "shdfg2323g3g4j3879sdfh2j3237w8eh";
                    var encryptedString = AesOperaions.EncryptString(key, user.u_password);
                    temp_user.u_full_name = user.u_full_name;
                    temp_user.u_name = user.u_name;
                    temp_user.u_is_admin = user.u_is_admin;
                    temp_user.u_role_id = user.u_role_id;
                    temp_user.u_active_yn = user.u_active_yn;
                    temp_user.u_password = encryptedString.ToString();
                    iuser.Update(temp_user);

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    ViewBag.RoleList = (irole.GetRoles);
                    ViewBag.DepartmentList = (idepartment.GetDepartments);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
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