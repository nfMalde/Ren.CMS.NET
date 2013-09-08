namespace Ren.CMS.CORE.CustomWebConfig.Accessors
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ren.CMS.CORE.CustomWebConfig.Handler;

    public static class RedirectIgnoreConfig
    {
        #region Methods

        public static RedirectIgnore GetConfig()
        {
            return ConfigurationManager.GetSection("redirectIgnore") as RedirectIgnore;
        }

        #endregion Methods
    }
}