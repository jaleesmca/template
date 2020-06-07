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
    public class UserDepartmentController : Controller
    {
        private readonly IUserDepartment iuserDepartment;
        private readonly IMenu imenu;
        private readonly IDepartment idepartment;
        private readonly IUser iuser;

        public UserDepartmentController(IUserDepartment _iuserDepartment,IMenu _imenu, IDepartment _idepartment, IUser _iuser)
        {
            iuserDepartment = _iuserDepartment;
            imenu = _imenu;
            idepartment = _idepartment;
            iuser = _iuser;
        }
        // GET: UserDepartment
        public ActionResult Index()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(iuserDepartment.GetUserDepartments);
            }
        }

        // GET: UserDepartment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserDepartment/Create
        public ActionResult Create()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.UserList = (iuser.GetUsers);
                ViewBag.DepartmentList = (idepartment.GetDepartments);
                return View();
            }
        }

        // POST: UserDepartment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDepartment userDepartment)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {

                    bool isExist = iuserDepartment.getIsExistOrNot(userDepartment.ud_user_id,userDepartment.ud_depart_id);
                    System.Diagnostics.Debug.WriteLine(userDepartment.ud_user_id + " "+isExist+" "+userDepartment.ud_depart_id);
                    if (isExist)
                    {
                        ViewBag.UserList = (iuser.GetUsers);
                        ViewBag.DepartmentList = (idepartment.GetDepartments);
                        TempData["errorMessage"] = "Already Exist!!";
                        return View();
                    }
                    else
                    {
                        userDepartment.ud_active_yn = "Y";
                        userDepartment.ud_cre_by = getCurrentUser().u_id;
                        userDepartment.ud_cre_date = DateTime.Now;
                        iuserDepartment.Add(userDepartment);

                        return RedirectToAction(nameof(Index));
                    }
                    

                }
                catch(Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                    ViewBag.UserList = (iuser.GetUsers);
                    ViewBag.DepartmentList = (idepartment.GetDepartments);
                    return View();
                }
            }
        }

        // GET: UserDepartment/Edit/5
        public ActionResult Edit(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.UserList = (iuser.GetUsers);
                ViewBag.DepartmentList = (idepartment.GetDepartments);
                return View(iuserDepartment.GetUserDepartment(id));
            }
        }

        // POST: UserDepartment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserDepartment _userDepartment)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    UserDepartment userDepartment=iuserDepartment.GetUserDepartment(id);
                    userDepartment.ud_user_id = _userDepartment.ud_user_id;
                    userDepartment.ud_depart_id = _userDepartment.ud_depart_id;
                    iuserDepartment.Update(userDepartment);

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                    ViewBag.UserList = (iuser.GetUsers);
                    ViewBag.DepartmentList = (idepartment.GetDepartments);
                    return View();
                }
            }
        }

        // GET: UserDepartment/Delete/5
        public ActionResult Delete(int id)
        {
            if (getCurrentUser() == null)
            {
                
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(iuserDepartment.GetUserDepartment(id));
            }
        }

        // POST: UserDepartment/Delete/5
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
                    UserDepartment userDepartment = iuserDepartment.GetUserDepartment(id);
                    iuserDepartment.Remove(id);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
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
                    ViewBag.Name = user.u_full_name;
                    ViewBag.isAdmin = user.u_is_admin;
                    List<MenuItems> menulist = new List<MenuItems>();

                    IEnumerable<Menu> menus = imenu.getMenulistByRoleAndType(user.u_role_id, "Menu");

                    foreach (var menu in menus)
                    {
                        MenuItems menuItems = new MenuItems();
                        menuItems.m_id = menu.m_id;
                        menuItems.m_description = menu.m_description;
                        menuItems.m_desc_to_show = menu.m_desc_to_show;
                        menuItems.m_link = menu.m_link;
                        menuItems.m_parrent_id = menu.m_parrent_id;
                        menuItems.m_type = menu.m_type;
                        menuItems.m_cre_by = menu.m_cre_by;
                        menuItems.m_active_yn = menu.m_active_yn;
                        menuItems.m_cre_date = menu.m_cre_date;
                        menuItems.menuItem = imenu.getMenulistByRoleAndTypeAndParrent(user.u_role_id, "MenuItem", menu.m_id);
                        menulist.Add(menuItems);
                    }

                    ViewBag.MenuList = menulist;


                    if (user.u_role_description.Equals("Monitor")) ViewBag.isMonitor = "Y";
                    else
                    {
                        ViewBag.isMonitor = "N";
                    }
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
