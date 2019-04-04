using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telekomunikacije.Models;
using Telekomunikacije.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Telekomunikacije.Controllers
{
    public class MembersController : Controller
    {
        // GET: Students
        private ApplicationDbContext _Context;

        public MembersController()
        {
            _Context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }
        public ActionResult Index()
        {
            return View();
            
        }
        
        public ActionResult GetMembers()
        {

            var users = _Context.Users.ToList();
            
            var usersRoles = new List<UserRoleViewModel>();

           foreach(var user in users)
            {
                var roles = new List<IdentityRole>();

                foreach(var item in user.Roles)
                {
                    roles.Add(_Context.Roles.Single(x => x.Id == item.RoleId));
                }
                var userRoles = new UserRoleViewModel
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    IdentityRoles = roles.ToList()
                    
                };
                usersRoles.Add(userRoles);
               
            }
                               
            return View(usersRoles);
            
        }
        public ActionResult MembersInfo(string id)
        {

            var user = _Context.Users.Single(x => x.Id == id);

           

            
          

          
            

            return View(user);

            
            

        }

        public ActionResult RoleInfo(string Id)
        {
            var user = _Context.Users.Single(x => x.Id == Id);
            
            
            var userRoles = new UserRoleViewModel
            {
                UserName = user.UserName,
                IdentityRoles = _Context.Roles.ToList(),
                Id = user.Id
            };

            return View(userRoles);
        }

        public ActionResult AddRole(UserRoleViewModel user)
        {

            
            //  System.Web.Security.Roles.AddUserToRole(usernameID, choosenRole);
            return RedirectToAction("Pomocni") ;
        }
        public ActionResult Pomocni()
        {

            var user = _Context.Users.Single(c => c.UserName == "Ilija");
            System.Web.Security.Roles.AddUserToRole(user.Id, "CanTeach");
            _Context.SaveChanges();
            return View();


        }
    }
}