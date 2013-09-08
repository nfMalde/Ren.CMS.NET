namespace Ren.CMS.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using Ren.CMS.Models.FormDialog;

    public static class FormDialogHelper
    {
        #region Methods

        public static IHtmlString CloseFormDialogFunc(this HtmlHelper helper, string elementID, bool includeEndlineDelimiter = true)
        {
            return new HtmlString("$('#" + elementID + "').dialog(\"close\")" + (includeEndlineDelimiter ? ";" : ""));
        }

        public static IHtmlString FormDialog(this HtmlHelper helper, FormDialogSettings Setup)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<object> Elements = new List<object>();

            foreach (FormDialogElement Element in Setup.Elements)
            {
                List<object> ThisStore = new List<object>();
                foreach (FormDialogDataStoreRow Row in Element.DataStore.Contents)
                {
                    Dictionary<string, object> row = new Dictionary<string, object>(){
                                                        {"label", Row.Label},
                                                        {"value", Row.Value}
                        };

                    ThisStore.Add(row);

                }

                if (Element.CustomRenderer != null) Element.CustomRenderer = "%" + Element.CustomRenderer + "%";
                Dictionary<object, object> element = new Dictionary<object, object>{
                                                     {"type", Element.Type},
                                                     {"label", Element.Label},
                                                     {"name", Element.Name},
                                                     {"id", Element.ID },
                                                     {"value", Element.Value},
                                                     {"required", Element.Required},
                                                     {"validators", Element.Validation},
                                                     {"customRenderer", Element.CustomRenderer},
                                                     {"customTemplate", Element.CustomTemplate},
                                                     {"dataStore", ThisStore}};

                Elements.Add(element);

            }

            Dictionary<object, object> _setup = new Dictionary<object, object>() {

            {"method", Setup.Method.ToString() },
            {"url", Setup.URL },
            {"title", Setup.Title},
            {"modal", Setup.Modal},
            {"width", Setup.Width},
            {"height", Setup.Height},
            {"elements", Elements },

            {"saveText", Setup.SaveText },

            {"abortText", Setup.AbortText },

            };

            string jscode = js.Serialize(_setup);
            jscode = jscode.Replace("\"%", "").Replace("%\"", "");
            string html = "<div id=\"" + Setup.ElementID + "\"></div>";
                   html+= "<script type=\"text/javascript\"> $(function(){ "+
                            "$('#"+ Setup.ElementID +"').formDialog("+
                            jscode +
                            ");"+
                            "});</script>";

                   return new HtmlString(html);
        }

        public static IHtmlString OpenFormDialogFunc(this HtmlHelper helper, string elementID, bool includeEndlineDelimiter = true)
        {
            return new HtmlString("$('#" + elementID + "').dialog(\"open\")" + (includeEndlineDelimiter ? ";" : ""));
        }

        #endregion Methods
    }
}