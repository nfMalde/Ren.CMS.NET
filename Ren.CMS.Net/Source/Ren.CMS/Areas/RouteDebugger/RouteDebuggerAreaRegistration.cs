namespace Ren.CMS.Areas.RouteDebugger
{
    using System.Web.Http;
    using System.Web.Mvc;

    public class RouteDebuggerAreaRegistration : AreaRegistration
    {
        #region Properties

        public override string AreaName
        {
            get
            {
                return "RouteDebugger";
            }
        }

        #endregion Properties

        #region Methods

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "RouteDebugger_default",
                "rd/{action}",
                new { controller = "RouteDebugger", action = "Index" });

            // Replace some of the default routing implementations with our custom debug
            // implementations.
            RouteDebuggerConfig.Register(GlobalConfiguration.Configuration);
        }

        #endregion Methods
    }
}