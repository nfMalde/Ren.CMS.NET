using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using nfCMS_NET.CORE;
using nfCMS_NET.MemberShip;
using nfCMS_NET.CORE.Settings;
namespace nfCMS_NET.Models.Backend.Layout
{

    public class DesktopModel
    {

        public string MenuBarTimeTitle { get; set; }

        public string MenuBarTime { get; set; }
    
    }

    public class MenuModel
    {

        public string UserName { get; set; }
        public string LiElements { get; set; }
    
    }
    
}