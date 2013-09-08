namespace Ren.CMS.MemberShip.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Security;

    public static class CmsUser
    {
        #region Fields

        public static MembershipUser Current = new nProvider.CurrentUser().nUser;

        #endregion Fields

        #region Methods

        public static MembershipUser Get(object PKID)
        {
            nProvider Prov =  (nProvider)Membership.Provider;
            return Prov.GetUser(PKID, false);
        }

        #endregion Methods
    }
}