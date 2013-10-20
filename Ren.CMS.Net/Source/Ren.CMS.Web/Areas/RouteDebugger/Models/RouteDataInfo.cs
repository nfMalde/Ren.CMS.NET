namespace Ren.CMS.Areas.RouteDebugger.Models
{
    using System.Collections.Generic;

    public class RouteDataInfo
    {
        #region Properties

        public KeyValuePair<string, string>[] Data
        {
            get; set;
        }

        public string RouteTemplate
        {
            get; set;
        }

        #endregion Properties
    }
}