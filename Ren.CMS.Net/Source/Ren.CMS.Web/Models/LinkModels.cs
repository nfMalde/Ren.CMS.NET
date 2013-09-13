namespace Ren.CMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Security;

    public class ForwardModel
    {
        #region Properties

        [Required]
        public string url
        {
            get; set;
        }

        #endregion Properties
    }
}