namespace Ren.CMS.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.CORE.Security;

    public static class UniqueIdentifierHelper
    {
        #region Fields

        private static string uniqueID = Guid.NewGuid().ToString();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Generates with the gived original ID Name an unique ID Name in the whole system.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="ElementID">The original Element ID</param>
        /// <returns>Unique ID</returns>
        public static string ProtectID(this HtmlHelper helper, string ElementID)
        {
            CryptoServices CS = new CryptoServices();

            string newID = "uID-"+CS.ConvertToSHA1(uniqueID + ElementID);

            return newID;
        }

        public static string ProtectJSFunc(this HtmlHelper helper, string funcName)
        {
            CryptoServices CS = new CryptoServices();
            return funcName + CS.ConvertToSHA1(funcName+uniqueID);
        }

        #endregion Methods
    }
}