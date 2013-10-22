namespace Ren.CMS.Areas.RouteDebugger.Models
{
    using System.Collections.Generic;

    public class RouteInfo
    {
        #region Properties

        public KeyValuePair<string, string>[] Constraints
        {
            get; set;
        }

        public KeyValuePair<string, string>[] DataTokens
        {
            get; set;
        }

        public KeyValuePair<string, string>[] Defaults
        {
            get; set;
        }

        public string Handler
        {
            get; set;
        }

        public bool Picked
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