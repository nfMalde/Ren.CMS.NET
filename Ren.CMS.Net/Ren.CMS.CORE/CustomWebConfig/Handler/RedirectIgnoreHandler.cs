namespace Ren.CMS.CORE.CustomWebConfig.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    using Ren.CMS.CORE.CustomWebConfig;

    public class RedirectIgnoreHandler : IConfigurationSectionHandler
    {
        #region Methods

        public object Create(object parent, object configContext, XmlNode section)
        {
            RedirectIgnore config = new RedirectIgnore();
            config.LoadValuesFromXml(section);

            return config;
        }

        #endregion Methods
    }
}