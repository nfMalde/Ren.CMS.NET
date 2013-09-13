namespace Ren.CMS.CORE.Helper.RoutingHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RoutingHelper
    {
        #region Methods

        //RouteCollection
        public static void MapRenCMSRoute(this RouteCollection collection,
            string routingName,
            string routingURL,
            object routingDefaults)
        {
            collection.MapRoute(routingName + "_LanguageInc",
                      "{renCMSLanguage}/"+routingURL,
                      routingDefaults);

            collection.MapRoute(routingName + "_Naked",
                      routingURL,
                      routingDefaults);
        }

        public static void MapRenCMSRoute(this RouteCollection collection,
            string routingName,
            string routingURL,
            object routingDefaults, string[] constraints)
        {
            collection.MapRoute(routingName + "_LanguageInc",
                      "{renCMSLanguage}/" + routingURL,
                      routingDefaults,constraints);

            collection.MapRoute(routingName + "_Naked",
                      routingURL,
                      routingDefaults, constraints);
        }

        //AreaRegistrationContext
        public static void MapRenCMSRoute(this AreaRegistrationContext collection,
            string routingName,
            string routingURL,
            object routingDefaults)
        {
            collection.MapRoute(routingName + "_LanguageInc",
                      "{renCMSLanguage}/" + routingURL,
                      routingDefaults);

            collection.MapRoute(routingName + "_Naked",
                      routingURL,
                      routingDefaults);
        }

        public static void MapRenCMSRoute(this AreaRegistrationContext collection,
            string routingName,
            string routingURL,
            object routingDefaults, string[] constraints)
        {
            collection.MapRoute(routingName + "_LanguageInc",
                      "{renCMSLanguage}/" + routingURL,
                      routingDefaults, constraints);

            collection.MapRoute(routingName + "_Naked",
                      routingURL,
                      routingDefaults, constraints);
        }

        #endregion Methods
    }
}