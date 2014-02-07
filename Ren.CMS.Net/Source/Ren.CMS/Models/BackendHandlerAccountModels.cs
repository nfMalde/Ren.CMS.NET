namespace Ren.CMS.Models.Backend.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Security;

    using Ren.CMS.CORE;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.MemberShip;

    public class Login
    {
        #region Properties

        [Required]
        public string uName
        {
            get; set;
        }

        [Required]
        public string uPassword
        {
            get; set;
        }

        #endregion Properties
    }

    public class RemoveIcon
    {
        #region Properties

        public int id
        {
            get; set;
        }

        #endregion Properties
    }
}