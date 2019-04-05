using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telekomunikacije.ViewModels;
using Telekomunikacije.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Telekomunikacije.ViewModels
{
    public class UserRoleManageViewModel
    {
        public UserRoleViewModel UserRoleViewModel  { get; set; }
        public IEnumerable<IdentityRole> AllRoles { get; set; }
    }
}