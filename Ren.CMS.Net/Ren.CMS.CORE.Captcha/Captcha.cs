using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ren.CMS.CORE.Captcha
{
    using System.IO;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;




    public class CaptchaValidatorAttribute : ActionFilterAttribute
    {


        private const string CHALLENGE_FIELD_KEY = "recaptcha_challenge_field";
        private const string RESPONSE_FIELD_KEY = "recaptcha_response_field";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var captchaChallengeValue = filterContext.HttpContext.Request.Form[CHALLENGE_FIELD_KEY];
            var captchaResponseValue = filterContext.HttpContext.Request.Form[RESPONSE_FIELD_KEY];
            var captchaValidtor = new Recaptcha.RecaptchaValidator
            {
                PrivateKey = "6Ldi688SAAAAALHKy2XQtksCChIJydt1G7Ry560e",
                RemoteIP = filterContext.HttpContext.Request.UserHostAddress,
                Challenge = captchaChallengeValue,
                Response = captchaResponseValue
            };

            var recaptchaResponse = captchaValidtor.Validate();

            // this will push the result value into a parameter in our Action  
            filterContext.ActionParameters["captchaValid"] = recaptchaResponse.IsValid;

            base.OnActionExecuting(filterContext);
        }
    }




    public class Captcha
    {

        private string pkey = "";
        private string pvkey = "";


        public Captcha(string publicKey, string privateKey)
        {

            this.pkey = publicKey;
            this.pvkey = privateKey;



        }

        public bool Verify(string challenge, string response)
        {
            bool ret = false;
            string postData = "";


            postData += HttpUtility.UrlEncode("privatekey") + "="
                  + HttpUtility.UrlEncode(this.pvkey) + "&";

            postData += HttpUtility.UrlEncode("remoteip") + "="
           + HttpUtility.UrlEncode(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]) + "&";
            postData += HttpUtility.UrlEncode("challenge ") + "="
       + HttpUtility.UrlEncode(challenge) + "&";
            postData += HttpUtility.UrlEncode("response  ") + "="
 + HttpUtility.UrlEncode(response) + "";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create("http://www.google.com/recaptcha/api/verify");
            myHttpWebRequest.Method = "POST";

            byte[] data = Encoding.ASCII.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = data.Length;

            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            Stream responseStream = myHttpWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

            string pageContent = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            responseStream.Close();

            myHttpWebResponse.Close();
            HttpContext.Current.Response.Write(pageContent);
            return pageContent.ToLower().StartsWith("true");

        }



    }



}
