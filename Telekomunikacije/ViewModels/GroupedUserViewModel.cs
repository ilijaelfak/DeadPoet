using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telekomunikacije.Models;

namespace Telekomunikacije.ViewModels
{
    public class GroupedUserViewModel
    {
        public List<UserRoleViewModel> Users { get; set; }
        public List<UserRoleViewModel> Admins { get; set; }
    }
}