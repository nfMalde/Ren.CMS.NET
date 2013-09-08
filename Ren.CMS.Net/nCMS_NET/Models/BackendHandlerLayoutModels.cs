namespace Ren.CMS.Models.Backend.Layout
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using Ren.CMS.CORE;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.MemberShip;

    public class changeBackground
    {
        #region Properties

        public string Align
        {
            get; set;
        }

        public string BGImage
        {
            get; set;
        }

        public string Color
        {
            get; set;
        }

        public string Repeat
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public List<object> StoreForAligns()
        {
            List<object> Store = new List<object>();

            Store.Add(new { id= "left", name = "Links"});
            Store.Add(new { id= "right", name = "Rechts"});
            Store.Add(new { id = "center", name = "Zentriert" });

            return Store;
        }

        public List<object> StoreForFiles()
        {
            List<object> Store = new List<object>();
            MembershipUser Cu = new MemberShip.nProvider.CurrentUser().nUser;
            Dictionary<string, string> store = new Dictionary<string, string>();
            store.Add("HTMLVAL", "TEXT");
            Store.Add(new { id = "none", name = "Kein Hintergrundbild" });
            string path2Custom = "/BackendFileHandler/CustomDesktops/" + Cu.ProviderUserKey;
            path2Custom = HttpContext.Current.Server.MapPath(path2Custom);

            if (!System.IO.Directory.Exists(path2Custom))
            {

                System.IO.Directory.CreateDirectory(path2Custom);

            }

            foreach (string file in System.IO.Directory.GetFiles(path2Custom))
            {

                Store.Add(new { id = System.IO.Path.GetFileName(file), name = System.IO.Path.GetFileName(file) });

            }

            return Store;
        }

        public List<object> StoreForRepeat()
        {
            List<object> Store = new List<object>();

            Store.Add(new { Repeat = "repeat", name = "Wiederholen" });
            Store.Add(new { Repeat = "repeat-x", name = "Horizontal wiederholen" });
            Store.Add(new { Repeat = "repeat-y", name = "Horizontal wiederholen" });
            Store.Add(new { Repeat = "no-repeat", name = "Nicht wiederholen" });

            return Store;
        }

        #endregion Methods
    }

    public class DesktopModel
    {
        #region Properties

        public string MenuBarTime
        {
            get; set;
        }

        public string MenuBarTimeTitle
        {
            get; set;
        }

        #endregion Properties
    }

    public class MenuModel
    {
        #region Properties

        public string LiElements
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        #endregion Properties
    }

    public class WidgetPost
    {
        #region Properties

        [Required]
        public string widget
        {
            get; set;
        }

        public string widgetHeaderData
        {
            get; set;
        }

        #endregion Properties
    }

    public class WidgetReturn
    {
        #region Properties

        public int definedHeight
        {
            get; set;
        }

        public int definedWidth
        {
            get; set;
        }

        public string Icon
        {
            get; set;
        }

        [Required]
        public int id
        {
            get; set;
        }

        [Required]
        public string neededPermission
        {
            get; set;
        }

        public string widgetHeaderData
        {
            get; set;
        }

        [Required]
        public string widgetName
        {
            get; set;
        }

        [Required]
        public string widgetPartialView
        {
            get; set;
        }

        public string widgetTitle
        {
            get; set;
        }

        #endregion Properties
    }
}