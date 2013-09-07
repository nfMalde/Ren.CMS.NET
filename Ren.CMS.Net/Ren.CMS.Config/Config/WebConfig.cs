using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace Ren.CMS.CORE.Config
{
    public static class RenConfig
    {
        public static partial class DB
        {
            private static NameValueCollection appsettings = ConfigurationManager.AppSettings;    

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
        }

       
    }
}
