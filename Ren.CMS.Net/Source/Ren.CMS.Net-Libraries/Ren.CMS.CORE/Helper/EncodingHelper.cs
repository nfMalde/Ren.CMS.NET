using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.CORE.Helper.FormEncoding
{
    public static class EncodingHelper
    {

        private static T doAction <T>(T @this, Action<T, PropertyInfo> action) where T:class,new()
        {
            var props = @this.GetType().GetProperties();

            if (props.Any(e => e.PropertyType == typeof(string) && e.GetValue(@this) != null))
            {
                foreach (var prop in props.Where(e => e.PropertyType == typeof(string) && e.GetValue(@this) != null))
                {

                    action(@this, prop);
                    
                }
            }



            return @this;


        }

 

        public static T EncodeStringsHTML<T>(this T @this) where T : class, new()
        {
            return doAction<T>(@this, (t,p) => {  p.SetValue(t, HttpUtility.HtmlEncode(p.GetValue(t).ToString())); } );
        }



        public static T DecodeStringsHTML<T>(this T @this) where T : class, new()
        {
            return doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.HtmlDecode(p.GetValue(t).ToString())); });
        }

        public static T EncodeStringURL<T>(this T @this) where T : class,new()
        {

            return doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.UrlEncode(p.GetValue(t).ToString())); });
        }

        public static T DecodeStringURL<T>(this T @this) where T : class,new()
        {

            return doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.UrlDecode(p.GetValue(t).ToString())); });
         }


        public static T EncodeStringsURLandHTML<T>(this T @this, bool htmlEncodeFirst = true) where T : class,new()
        {
            Action<PropertyInfo> _encodeHTML = e => e.SetValue(@this, HttpUtility.HtmlEncode(e.GetValue(@this).ToString()));
            Action<PropertyInfo> _encodeURL = e => e.SetValue(@this, HttpUtility.UrlEncode(e.GetValue(@this).ToString()));

            if (htmlEncodeFirst)
            {

                @this = doAction<T>(@this, (t,p) => {  p.SetValue(t, HttpUtility.HtmlEncode(p.GetValue(t).ToString())); } );
                @this = doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.UrlEncode(p.GetValue(t).ToString())); });
 

            }

            else
            {
                @this = doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.UrlEncode(p.GetValue(t).ToString())); });
                @this = doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.HtmlEncode(p.GetValue(t).ToString())); });
            }



            return @this;


        }


        public static T DecodeStringsURLandHTML<T>(this T @this, bool htmlDecodeFirst = true) where T : class,new()
        {
            Action<PropertyInfo> _decodeHTML = e => e.SetValue(@this, HttpUtility.HtmlDecode(e.GetValue(@this).ToString()));
            Action<PropertyInfo> _decodeURL = e => e.SetValue(@this, HttpUtility.UrlDecode(e.GetValue(@this).ToString()));

            if (htmlDecodeFirst)
            {

                @this = doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.HtmlDecode(p.GetValue(t).ToString())); });
                @this = doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.UrlDecode(p.GetValue(t).ToString())); });


            }

            else
            {
                @this = doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.UrlDecode(p.GetValue(t).ToString())); });
                @this = doAction<T>(@this, (t, p) => { p.SetValue(t, HttpUtility.HtmlDecode(p.GetValue(t).ToString())); });
            }



            return @this;


        }

    }
}
