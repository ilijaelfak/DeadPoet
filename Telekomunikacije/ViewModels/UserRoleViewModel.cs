using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telekomunikacije.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Telekomunikacije.ViewModels
{
    public class UserRoleViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public  List<IdentityRole> IdentityRoles { get; set; }
    }
}