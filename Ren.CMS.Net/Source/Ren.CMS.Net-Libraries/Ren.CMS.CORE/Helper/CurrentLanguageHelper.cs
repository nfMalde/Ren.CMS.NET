namespace Ren.CMS.CORE.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.Persistence.Base;
    using Ren.CMS.Persistence.Domain;

    public static class CurrentLanguageHelper
    {
        #region Properties

        public static string CurrentLanguage
        {
            get {

                string language = DefaultLanguage;

                try
                {

                    language = GetLanguageRoutData();

                    BaseRepository<LangCode> L = new BaseRepository<LangCode>();
                    var lc = L.GetOne(NHibernate.Criterion.Expression.Where<LangCode>(e => e.Code == language));
                    language = lc.Code ?? DefaultLanguage;

                }
                catch { }

                return language;

            }
        }

        public static string DefaultLanguage
        {
            get
            {

                SettingsHelper.GlobalSettingsHelper GS = new SettingsHelper.GlobalSettingsHelper();

                string l = GS.Read("GLOBAL_DEFAULT_LANGUAGE");

                return l;

            }
        }

        #endregion Properties

        #region Methods

        public static string GetLanguageRoutData()
        {
            var routeData = ((MvcHandler)HttpContext.Current.Handler).RequestContext.RouteData;
            string language = null;
            if (routeData.Values["renCMSLanguage"] != null)
                language = (string)routeData.Values["renCMSLanguage"] ?? null;

            return language;
        }

        #endregion Methods
    }
}