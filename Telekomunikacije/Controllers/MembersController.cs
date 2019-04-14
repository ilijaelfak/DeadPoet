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
using System.Threading.Tasks;
using System.Net;

namespace Telekomunikacije.Controllers
{
    [Authorize]
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
            var editUser = new EditViewModel
            {
                UserName = user.UserName,
                IndexNumber = user.IndexNumber,
                Email = user.Email,
                Id = user.Id
            };
            return View(editUser);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("MembersInfo",user);
            }

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_Context));
            var userInDb = userManager.FindById(user.Id);

            userInDb.UserName = user.UserName;
            userInDb.IndexNumber = user.IndexNumber;          
            userInDb.Email = user.Email;

            userManager.Update(userInDb);

            return RedirectToAction("GetMembers");
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
            ViewBag.ResultMessage1 = "Rola je uspesno dodata";

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
                ViewBag.ResultMessage2 = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage2 = "This user doesn't belong to selected role.";
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


        public async Task<ActionResult> DeleteConfirmed(string id)
        {
                

                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_Context));
                         
                var user = await manager.FindByIdAsync(id);
               
                var rolesForUser = await manager.GetRolesAsync(id);

                using (var transaction = _Context.Database.BeginTransaction())
                {

                if (rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            
                            var result = await manager.RemoveFromRoleAsync(user.Id, item);
                        }
                    }

                    await manager.DeleteAsync(user);
                    transaction.Commit();
                }

                return RedirectToAction("Index");
           
                
            
        }


        public ActionResult Pomocni()
        {

            

            return View();



        }
    }
}