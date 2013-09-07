using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Ren.CMS.CORE;
using Ren.CMS.MemberShip;
using Ren.CMS.CORE.Settings;
namespace Ren.CMS.Models.Backend.Account
{

    public class RemoveIcon
    {

        public int id { get; set; }
    }


    public class Login
    {
        [Required]
        public string uName { get; set; }

        [Required]
        public string uPassword { get; set; }
    
    }
    
}