using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Text;
using System.IO;
namespace nfCMS_NET.Helpers
{
    public static class HtmlExtensions 
    {
        public static IHtmlString GenerateCaptcha(this HtmlHelper helper)
        {

            var captchaControl = new Recaptcha.RecaptchaControl
            {
                ID = "recaptcha",
                Theme = "blackglass",
                PublicKey = "6Ldi688SAAAAADImB2IsQPOD7GpdCZzEUoqWm4DZ",
                PrivateKey = "6Ldi688SAAAAALHKy2XQtksCChIJydt1G7Ry560e"
            };

            var htmlWriter = new HtmlTextWriter(new StringWriter());

            captchaControl.RenderControl(htmlWriter);

            return helper.Raw(htmlWriter.InnerWriter.ToString());
        }

        //Adds an Ajax Link, 
        public static IHtmlString nAjaxLink(this HtmlHelper helper, string text, string controller, string action, string callbackfunc = "", string contentID = "content")
        {
            if (!String.IsNullOrEmpty(callbackfunc))
            {
                callbackfunc += ", " + callbackfunc;
            
            }
            string script = "<script type=\"text/javascript\">";

            nfCMS_NET.CORE.Security.CryptoServices Crypto = new CORE.Security.CryptoServices();
            string id = "HT_ID_"+ Crypto.ConvertToSHA1(DateTime.Now.ToShortTimeString() + DateTime.Now.Millisecond.ToString());
            script += "$(function(){  makeAjaxLink('" + contentID + "','" + id + "'" + callbackfunc + "); });";
            script += "</script>";

            string a = "<a href=\"/" + controller + "/" + action + "\" id=" + id + ">" + text + "</a>";

            string html = script + a;

            return helper.Raw(html);
        
        }

    }
}