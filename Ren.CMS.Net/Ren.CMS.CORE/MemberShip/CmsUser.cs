using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Ren.CMS.MemberShip.Helper
{
    public static class CmsUser
    {
        public static MembershipUser Current = new nProvider.CurrentUser().nUser;
        public static MembershipUser Get(object PKID) 
        { 
            nProvider Prov =  (nProvider)Membership.Provider;
            return Prov.GetUser(PKID, false);
        }
    }
}
