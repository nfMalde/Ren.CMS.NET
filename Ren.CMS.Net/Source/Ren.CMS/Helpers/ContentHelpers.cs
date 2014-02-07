namespace Ren.CMS.Helpers.Content
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    using Ren.CMS.Content;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.MemberShip;

    public static class ContentHelpers
    {
        #region Methods

        public static bool CreatedByregistieredUser(this nContent content)
        {
            GlobalSettings globalSettings = new GlobalSettings();

            nSetting GuestPKID = globalSettings.getSetting("GLOBAL_GUESTPKID");
            string guestPKID = GuestPKID.Value.ToString().ToUpper();

            if (content.CreatorPKID.ToString().ToUpper() == guestPKID)
                return false;

            MembershipUser User = Membership.GetUser(providerUserKey: content.CreatorPKID, userIsOnline:false);
            if (User == null)
            {

                return false;
            }

            return true;
        }

        #endregion Methods
    }
}