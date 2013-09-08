namespace Ren.CMS.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.CORE.Language.LanguageDefaults;
    using Ren.CMS.CORE.Security;

    public static class LanguageHelper
    {
        #region Methods

        /// <summary>
        /// Returns quickly an language line as HTML String.
        /// </summary>
        /// <param name="helper">Attached to the HTML Helper</param>
        /// <param name="LangLineName">Name of the language line</param>
        /// <param name="Package">Language Package Name</param>
        /// <param name="DefaultReturnValue">Dictionary with defaultvalues for the Database (string LanguageCode(this is the key),string LineContent (this is the Value)) If a langcode in the database does not exists in the dictionary the first value will be used.</param>
        /// <param name="LangCode">Language Code (example "de-DE" for german)</param>
        /// <returns></returns>
        public static HtmlString LanguageLine(this HtmlHelper helper, LanguageDefaultValues DefaultReturnValue)
        {
            return new HtmlString(DefaultReturnValue.ReturnLangLine());
        }

        /// <summary>
        /// Returns quickly an language line as HTML String.
        /// </summary>
        /// <param name="helper">Attached to the HTML Helper</param>
        /// <param name="LangLineName">Name of the language line</param>
        /// <param name="Package">Language Package Name</param>
        /// <param name="DefaultReturnValue">Dictionary with defaultvalues for the Database (string LanguageCode(this is the key),string LineContent (this is the Value)) If a langcode in the database does not exists in the dictionary the first value will be used.</param>
        /// <param name="LangCode">Language Code (example "de-DE" for german)</param>
        /// <returns></returns>
        public static HtmlString LanguageLine(this HtmlHelper helper, string LangLineName, string Package = "Root",  Dictionary<string,string> DefaultReturnValue = null, string LangCode = "__USER__")
        {
            Ren.CMS.CORE.Language.Language Lang = new CORE.Language.Language(LangCode, Package);
            string returnValue = Lang.getLine(LangLineName, DefaultReturnValue);

            return new HtmlString(returnValue);
        }

        #endregion Methods
    }
}