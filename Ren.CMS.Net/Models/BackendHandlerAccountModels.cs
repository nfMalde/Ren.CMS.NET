using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using nfCMS_NET.CORE;
using nfCMS_NET.MemberShip;
using nfCMS_NET.CORE.Settings;
namespace nfCMS_NET.Models.Backend.Account
{

    public class Login
    {
        [Required]
        public string uName { get; set; }

        [Required]
        public string uPassword { get; set; }
    
    }
    
}