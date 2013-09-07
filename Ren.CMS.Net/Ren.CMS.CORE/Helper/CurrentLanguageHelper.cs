using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ren.CMS.CORE.nhibernate.Domain;
using Ren.CMS.CORE.nhibernate.Base;
using System.Web;
using System.Web.Mvc;



namespace Ren.CMS.CORE.Helper
{
    public static class CurrentLanguageHelper
    {
        public static string CurrentLanguage {

            get {

                string language = DefaultLanguage;

                try
                {
                    var routeData = ((MvcHandler)HttpContext.Current.Handler).RequestContext.RouteData;

                    if (routeData.Values["renCMSLanguage"] != null)
                        language = (string)routeData.Values["renCMSLanguage"] ?? DefaultLanguage;

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







    }
}
