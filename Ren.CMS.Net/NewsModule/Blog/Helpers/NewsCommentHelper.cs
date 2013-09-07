using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ren.CMS.Content;
using Ren.CMS.CORE.Settings;
using Ren.CMS.Blog.Models;

namespace Ren.CMS.Blog.Helpers
{
 
    public static class NewsCommentHelper
    {
        private static string GuestPKID = new GlobalSettings().getSetting("GLOBAL_GUESTPKID").Value.ToString();
        public static ModelStateDictionary RevalidateCommentFields(this ModelStateDictionary modelstate, NewsComment comment)
        {
            MembershipUser User = Membership.GetUser(username: comment.Nickname, userIsOnline: false);

            if (User != null && !HttpContext.Current.Request.IsAuthenticated)
            {

                modelstate.AddModelError("form","Dieser Benutzername wird bereits von einem registrierten Mitglied verwendet.");
            }

            return modelstate;
        
        }
        public static ModelStateDictionary ValidateCaptcha(this ModelStateDictionary modelstate, bool captchaValidProperty, string captchaErrorProperty)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {

                //Try to get User...

                


                if (!captchaValidProperty)
                {
                    modelstate.AddModelError("captcha", captchaErrorProperty);


                }
            }
            return modelstate;
        }
        public static string SpecialNameForGuests(nContent a)
        {
            if (a.CreatorPKID.ToString().ToLower() == GuestPKID.ToLower())
                return a.CreatorSpecialName;


            return a.CreatorName;

        }
    }
}