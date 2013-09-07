using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Text;
using System.IO;
namespace Ren.CMS.Helpers
{
    
   



    public static class HtmlExtensions 
    {

        public static IHtmlString ScrollTo(this HtmlHelper helper,  string elementID)
        {

          

            TagBuilder Builder = new TagBuilder("script");
            Builder.Attributes.Add("type", "text/javascript");
            StringBuilder InnerHtml = new StringBuilder();

            InnerHtml.AppendLine();
            InnerHtml.AppendLine("$(function(){").AppendLine();

            InnerHtml.AppendFormat("window.location.hash = '{0}';", elementID);
            InnerHtml.AppendLine();

            InnerHtml.AppendLine("});");
            InnerHtml.AppendLine();

            Builder.InnerHtml = InnerHtml.ToString();


            return new HtmlString(Builder.ToString());
        
        }

        public static MvcHtmlString ChosenBox<TModel>(this HtmlHelper helper, string name, string dataField, string valueField, IEnumerable<TModel> Items, IDictionary<string, object> htmlAttributes = null, object config = null)
        {
            var select = new TagBuilder("select");
            select.Attributes.Add("name", name);
            select.Attributes.Add("id", name);
            select.MergeAttributes(htmlAttributes);

            foreach (var item in Items)
            {
                var option = new TagBuilder("option");
                option.Attributes.Add("value", typeof(TModel).GetProperty(valueField).GetValue(null).ToString());
                option.SetInnerText(typeof(TModel).GetProperty(dataField).GetValue(null).ToString());
                select.InnerHtml += option.ToString();
            }

         

            var script = new TagBuilder("script");
            script.Attributes.Add("type", "text/javascript");

            StringBuilder ScriptText = new StringBuilder();
            ScriptText.AppendLine("$(function() {")
                      .AppendLine()
                      .AppendFormat("$('{0}').chosen();", select.Attributes["id"].ToString())
                      .AppendLine()
                      .AppendLine("});");


            script.InnerHtml = ScriptText.ToString();




            return MvcHtmlString.Create(select.ToString() + script.ToString());

        }
/*
        public static IHtmlString GenerateCaptcha(this HtmlHelper helper)
        {

            var captchaControl = new Recaptcha.RecaptchaControl
            {
                ID = "recaptcha_" + Guid.NewGuid(),
                Theme = "blackglass",
                PublicKey = "6Ldi688SAAAAADImB2IsQPOD7GpdCZzEUoqWm4DZ",
                PrivateKey = "6Ldi688SAAAAALHKy2XQtksCChIJydt1G7Ry560e"
            };

            var htmlWriter = new HtmlTextWriter(new StringWriter());

            captchaControl.RenderControl(htmlWriter);

            return helper.Raw(htmlWriter.InnerWriter.ToString());
        }
        */
        //Adds an Ajax Link, 
        public static IHtmlString nAjaxLink(this HtmlHelper helper, string text, string controller, string action, string callbackfunc = "", string contentID = "content")
        {
            if (!String.IsNullOrEmpty(callbackfunc))
            {
                callbackfunc += ", " + callbackfunc;
            
            }
            string script = "<script type=\"text/javascript\">";

            Ren.CMS.CORE.Security.CryptoServices Crypto = new CORE.Security.CryptoServices();
            string id = "HT_ID_"+ Crypto.ConvertToSHA1(DateTime.Now.ToShortTimeString() + DateTime.Now.Millisecond.ToString());
            script += "$(function(){  makeAjaxLink('" + contentID + "','" + id + "'" + callbackfunc + "); });";
            script += "</script>";

            string a = "<a href=\"/" + controller + "/" + action + "\" id=" + id + ">" + text + "</a>";

            string html = script + a;

            return helper.Raw(html);
        
        }

        public static void AddScriptFile(this HtmlHelper helper, string url)
        {
            helper.ViewData["______SCRIPT"+ Guid.NewGuid().ToString()] = url;

        
        }

        public static void AddCSSFile(this HtmlHelper helper, string url)
        {

            helper.ViewData["______CSS" + Guid.NewGuid().ToString()] = url;
            
        
        }

        public static IHtmlString RenderScriptFiles(this HtmlHelper helper)
        {   
            var Dict = helper.ViewData.Where(e => e.Key.StartsWith("______SCRIPT"));
            if (Dict == null) return new HtmlString("");
            StringBuilder Builder = new StringBuilder();
            foreach (KeyValuePair<string, object> Pair in Dict)
            {
          
                Builder.AppendFormat("<script type=\"text/javascript\" src=\"{0}\">", Pair.Value.ToString()).AppendLine("</script>").AppendLine();

            
            }

            return new HtmlString(Builder.ToString());
        
        }

        public static IHtmlString RenderCSSFiles(this HtmlHelper helper)
        {
            var Dict = helper.ViewData.Where(e => e.Key.StartsWith("______SCRIPT"));
            if (Dict == null) return new HtmlString("");
            StringBuilder Builder = new StringBuilder();
            foreach (KeyValuePair<string, object> Pair in Dict)
            {
        
                Builder.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />", Pair.Value.ToString()).AppendLine();


            }

            return new HtmlString(Builder.ToString());
        
        }
    }
}