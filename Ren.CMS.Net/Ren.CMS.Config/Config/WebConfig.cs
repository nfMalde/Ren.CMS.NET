namespace Ren.CMS.CORE.Config
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class RenConfig
    {
        #region Nested Types

        public static partial class DB
        {
            #region Fields

            private static NameValueCollection appsettings = ConfigurationManager.AppSettings;

            #endregion Fields

            #region Properties

            public static string Prefix
            {
                get
                {
                    if (appsettings.Get("nfcmsSQLPrefix") != null)
                    {

                        return appsettings.Get("nfcmsSQLPrefix").ToString();

                    }

                    return String.Empty;

                }
                set
                {
                    appsettings.Set("nfcmsSQLPrefix", value);

                }
            }

            #endregion Properties
        }

        #endregion Nested Types
    }
}