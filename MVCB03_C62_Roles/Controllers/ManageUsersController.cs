using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCB03_C62_Roles.Models;
using MVCB03_C62_Roles.ViewModels;

namespace MVCB03_C62_Roles.Controllers
{
    public class ManageUsersController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;
        public ManageUsersController()
        {

        }
        public ManageUsersController( ApplicationUserManager userManager,ApplicationDbContext context)
        {
          
            _userManager = userManager;
            _context = context;
        }
        public ApplicationDbContext Context
        {
            get
            {
                return _context ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            private set
            {
                _context = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [Authorize(Roles ="Admin")]
        public ActionResult AssignUSerToRoll(string userId)
        {
            ViewBag.Role = new SelectList(Context.Roles.OrderBy(r => r.Name).ToList(), "Name", "Name");
            var user = UserManager.FindById(userId);
            UserRoleVM usertoAssign = new UserRoleVM
            {
                Email = user.Email,
                Username = user.UserName,
                UserId = user.Id,
            };
            return View(usertoAssign);
        }

            [HttpPost]
        public ActionResult AssignUSerToRoll(UserRoleVM userRoleVM, string Role)
        {
            var result = UserManager.AddToRole(userRoleVM.UserId, Role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            if (result.Errors.Count() > 0)
            {
                string m = "";
                foreach (var error in result.Errors)
                {
                    m += error + ",";
                }
                m = m.Substring(m.Length - 1);
                ViewBag.msg = m;
            }
            return View();
        }
        [HttpGet]
        public ActionResult RemoveUSerToRoll(string UserId)
        {
            ViewBag.Role = new SelectList(Context.Roles.OrderBy(r => r.Name).ToList(), "Name", "Name");
            var user = UserManager.FindById(UserId);
            UserRoleVM usertoAssign = new UserRoleVM
            {
                Email = user.Email,
                Username = user.UserName,
                UserId = user.Id,
            };
            return View(usertoAssign);
           
        }
            [HttpPost]
        public ActionResult RemoveUSerToRoll( string UserId, string Role)
        {
            var result = UserManager.RemoveFromRole(UserId, Role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            if (result.Errors.Count() > 0)
            {
                string m = "";
                foreach (var error in result.Errors)
                {
                    m += error + ",";
                }
                m = m.Substring(m.Length - 1);
                ViewBag.msg = m;
            }
            return View();
        }

        // GET: ManageUsers
        public ActionResult Index()
        {
            var usersWithRoles = (from user in Context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in Context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserRoleVM()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            return View(usersWithRoles);
        }
    }
}