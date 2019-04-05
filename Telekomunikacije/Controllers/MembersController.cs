using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telekomunikacije.Models;
using Telekomunikacije.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

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

            var roles = new List<IdentityRole>();

            foreach (var item in user.Roles)
            {
                roles.Add(_Context.Roles.Single(x => x.Id == item.RoleId));
            }
            
            var userRoles = new UserRoleViewModel
            {
                UserName = user.UserName,
                IdentityRoles = roles.ToList(),
                Id = user.Id
            };
            var userRolesManage = new UserRoleManageViewModel
            {
                UserRoleViewModel = userRoles,
                AllRoles = _Context.Roles.ToList()
            };
            return View(userRolesManage);
        }

        public ActionResult RoleAddToMember(UserRoleManageViewModel user,string Id)
        {

            var userInDb = _Context.Users.Single(x => x.Id == user.UserRoleViewModel.Id);

            var roles = new List<IdentityRole>();

            foreach (var item in userInDb.Roles)
            {
                roles.Add(_Context.Roles.Single(x => x.Id == item.RoleId));
            }
            var roleName =_Context.Roles.Single(x => x.Id == Id).Name;

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_Context));
            manager.AddToRole(user.UserRoleViewModel.Id, roleName);

            //var id = user.UserRoleViewModel.Id;
            var p = new UserRoleViewModel
            {
                UserName = user.UserRoleViewModel.UserName,
                IdentityRoles = roles.ToList(),
                Id = user.UserRoleViewModel.Id
            };
            var p1 = new UserRoleManageViewModel
            {
                AllRoles = _Context.Roles.ToList(),
                UserRoleViewModel = p
            };
            return View("RoleInfo", p1);
        }

        public ActionResult GetRoles(UserRoleManageViewModel user)
        {

            var userInDb = _Context.Users.Single(x => x.Id == user.UserRoleViewModel.Id);

            var roles = new List<IdentityRole>();

            foreach (var item in userInDb.Roles)
            {
                roles.Add(_Context.Roles.Single(x => x.Id == item.RoleId));
            }

            //var id = user.UserRoleViewModel.Id;
            var p = new UserRoleViewModel
            {
                UserName = user.UserRoleViewModel.UserName,
                IdentityRoles = roles.ToList(),
                Id = user.UserRoleViewModel.Id
            };
            var p1 = new UserRoleManageViewModel
            {
                AllRoles = _Context.Roles.ToList(),
                UserRoleViewModel = p
            };
            return View("RoleInfo",p1);
            
        }

        public ActionResult DeleteRoleForMember(UserRoleManageViewModel user, string Id)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_Context));

            var roleName = _Context.Roles.Single(x => x.Id == Id).Name;

            if (manager.IsInRole(user.UserRoleViewModel.Id, roleName))
            {
                manager.RemoveFromRole(user.UserRoleViewModel.Id, roleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            var userInDb = _Context.Users.Single(x => x.Id == user.UserRoleViewModel.Id);

            var roles = new List<IdentityRole>();

            foreach (var item in userInDb.Roles)
            {
                roles.Add(_Context.Roles.Single(x => x.Id == item.RoleId));
            }

            //var id = user.UserRoleViewModel.Id;
            var p = new UserRoleViewModel
            {
                UserName = user.UserRoleViewModel.UserName,
                IdentityRoles = roles.ToList(),
                Id = user.UserRoleViewModel.Id
            };
            var p1 = new UserRoleManageViewModel
            {
                AllRoles = _Context.Roles.ToList(),
                UserRoleViewModel = p
            };
            return View("RoleInfo", p1);
        }
        public ActionResult Pomocni()
        {

            var id = _Context.Users.Single(x => x.UserName == "Admin").Id;
            var user = new UserRoleViewModel
            {
                Id = id,
                UserName = "Admin",
                IdentityRoles = _Context.Roles.ToList()

             };
            var userRoleVM = new UserRoleManageViewModel
            {
                AllRoles = _Context.Roles.ToList(),
                UserRoleViewModel = user
            };

            return View("RoleInfo", user);



        }
    }
}