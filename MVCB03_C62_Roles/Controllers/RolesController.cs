using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCB03_C62_Roles.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationRoleManager _roleManager;
       
        public RolesController()
        {

        }
        public RolesController(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
            
        }
    

        public ApplicationRoleManager ApplicationRoleManager
        {
            get { 
                //if (_roleManager != null) {
                //    return _roleManager; }
                //else
                //{
                //    return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
                 
                //}
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            set
            {
                _roleManager = value;
            }
        }
        // GET: Roles
        public ActionResult Index()
        {
            return View(ApplicationRoleManager.Roles.ToList());
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(string rolename)
        {
            var result = ApplicationRoleManager.Create(new IdentityRole{ 
             Name=rolename});
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            if (result.Errors.Count() > 0)
            { string m = "";
                foreach(var error in result.Errors)
                {
                    m += error +",";
                }
                m=m.Substring(m.Length-1);
                ViewBag.msg = m;
            }
            return View();
        }
     
    }
}