namespace Ren.CMS.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.CORE.Security;
    using Ren.CMS.Models.jsTreeModels;

    public static class jsTreeHelper
    {
        #region Fields

        public static string lastID = null;

        private static string lastElementID = "";

        #endregion Fields

        #region Methods

        public static HtmlString jsTreeFull(this HtmlHelper helper, jsTreeSettings settings, string elID = null, bool withTags = true)
        {
            elID = simID(elID);
            string script =  buildTreeScript(elID,settings);
            script = fixScript(script, withTags);

            string HTML = "<div id=\"" + elID + "\"></div>" + script;

            return new HtmlString(HTML);
        }

        public static HtmlString jsTreeScriptOnly(this HtmlHelper helper, jsTreeSettings settings, string elID = null, bool withTags=true)
        {
            elID = simID(elID);

            string script = buildTreeScript(elID, settings);

            script = fixScript(script, withTags);

            return new HtmlString(script);
        }

        private static string buildTreeScript(string id, jsTreeSettings settings)
        {
            //Create UL

            string html = "";

            html+= "$('#"+ HttpContext.Current.Server.HtmlDecode( id ) +"').jstree({";

            //Plugins
            html+= "\"plugins\" : [";

            if(settings.plugins != null)
            {
                string plugins = "";

                foreach(string plugin in settings.plugins.Plugins)
                {

                    if(plugins != "") plugins+= ",";

                    plugins+= "\"";

                    plugins+= HttpContext.Current.Server.HtmlDecode(plugin);

                    plugins+= "\"";

                }
             html+=plugins;
            }

            html+="],";

            //Tree Data
            //Read
            if(settings.ajax_read != null && !String.IsNullOrEmpty(settings.ajax_read.url))
            {
                html+="\"";
                html+= HttpContext.Current.Server.HtmlDecode(settings.ajax_read.dataType);
                html+="\" : {";

                html+="\"ajax\": {";
                html += "\"type\": \"" + settings.ajax_read.method + "\",";
                html+="\"url\":";

                if (settings.ajax_read.UrlIsJavaScript)
                {
                    html +=  settings.ajax_read.url;
                }
                else
                {
                    html += "\"" + settings.ajax_read.url + "\",";
                }
                html+= "\"data\": function(n) {";
                html+= " return { ";
                html+= " \"operation\" : \"ajax_read\",";

                html+= " \"node_id\" : n.attr ? n.attr(\"id\").replace(\"node_\",\"\") : 0";

                string additionalParametersRead = "";
                if(settings.ajax_read.additionalRequestParameters != null)
                {
                    foreach(KeyValuePair<string, string> par in settings.ajax_read.additionalRequestParameters)
                    {
                        additionalParametersRead+=",";

                        additionalParametersRead+="\""+ HttpUtility.HtmlDecode(par.Key) +"\" : ";
                        if (par.Value.StartsWith("jsCode::"))
                        {
                            additionalParametersRead += par.Value.Substring(8);

                        }
                        else
                        {
                            additionalParametersRead += "\"" + HttpUtility.HtmlDecode(par.Value) + "\"";
                        }

                    }
                }
                html+=additionalParametersRead;
                html += "}";
                html += "}";

                //Search

                html+="}";
                html += "},";

            }

            if(settings.ajax_search != null &&  !String.IsNullOrEmpty(settings.ajax_search.url))
            {
                html+="\"";
                html+= "search";
                html+="\" : {";

                html+="\"ajax\": {";

                html+="\"url\":";

                if (settings.ajax_search.UrlIsJavaScript)
                {
                    html += settings.ajax_search.url;
                }
                else
                {
                    html += "\"" + settings.ajax_search.url + "\",";
                }
                html+= "\"data\": function(str) {";
                html+= " return { ";
                html+= " \"operation\" : \"ajax_read\",";

                html+= " \"search_str\" : str";

                string additionalParametersSearch = "";
                if(settings.ajax_search.additionalRequestParameters != null)
                {
                    foreach(KeyValuePair<string, string> par in settings.ajax_search.additionalRequestParameters)
                    {
                        additionalParametersSearch+=",";

                        additionalParametersSearch+="\""+ HttpUtility.HtmlDecode(par.Key) +"\" : ";

                        additionalParametersSearch+="\""+ HttpUtility.HtmlDecode(par.Value)+"\"";

                    }
                }
                html+=additionalParametersSearch;
                //Search

                html+="},";
            }
            //TODO: Edit, Create, Delete

            //Types

            jsTreeTypes Types = settings.types;

            html+= "\"types\" : {";

                html+= "\"max_depth\" : "+ Types.max_depth +",";

                html+= "\"max_children\" : "+ Types.max_children +",";

                html+= "\"valid_children\" : [";

                string valid_children = "";

                if(Types.valid_children != null)
                {

                    foreach(string vc in Types.valid_children)
                    {
                        if(valid_children != "") valid_children+=", ";

                        valid_children+= "\""+ HttpUtility.HtmlDecode(vc)+"\"";

                    }

                }
              html+= valid_children;

              html+="],";

             html+="\"types\" : {";

             if (Types.types != null)
             {
                 string myTypes = "";
                 foreach (jsTreeType Type in Types.types)
                 {
                     if (myTypes != "") myTypes += ", ";

                     myTypes += "\"" + Type.name + "\": {";

                     myTypes += "\"valid_children\" : [";

                     string vc = "";

                     foreach (string v in Type.valid_children)
                     {
                         if (vc != "")
                         {
                             vc += ", ";

                         }

                         vc += "\"" + HttpUtility.HtmlDecode(v) + "\"";

                     }
                     myTypes += vc;
                     myTypes += "],";

                     myTypes += "\"icon\" : {";

                     myTypes += "\"image\" : \"" + Type.icon + "\",";

                     myTypes += "}";

                     myTypes += "}";

                 }
                 html += myTypes;

             }
             html += "}";
                 if (settings.ui != null)
                 {

                     html += ", \"ui\" : {";

                     string ui = "";
                     foreach (KeyValuePair<string, object> p in settings.ui)
                     {

                         object valuex = p.Value;
                         if (ui != "") ui += ", ";

                         ui += "\"" + p.Key + "\" : ";

                         if (valuex.GetType() == typeof(string[]))
                         {

                             ui += "[";

                             string strarray = "";

                             foreach (string str in (string[])valuex)
                             {

                                 if (strarray != "")
                                 {
                                     strarray += ",";
                                 }
                                 strarray += "\"" + str + "\"";

                             }
                             ui += strarray;

                             ui += "]";

                         }
                         else if (valuex.GetType() == typeof(int))
                         {
                             ui += (int)valuex;
                         }
                         else
                         {

                             ui += "\"" + valuex.ToString() + "\"";

                         }

                     }
                     html += ui;

                     html += "}";

                 }

                 //Core
                 if (settings.core != null)
                 {

                     html += ", \"core\" : {";

                     string core = "";
                     foreach (KeyValuePair<string, object> p in settings.core)
                     {

                         object valuex = p.Value;
                         if (core != "") core += ", ";

                         core += "\"" + p.Key + "\" : ";

                         if (valuex.GetType() == typeof(string[]))
                         {

                             core += "[";

                             string strarray = "";

                             foreach (string str in (string[])valuex)
                             {

                                 if (strarray == "")
                                 {
                                     strarray += ",";
                                 }
                                 strarray += "\"" + str + "\"";

                             }
                             core += strarray;
                             core += "]";

                         }
                         else if (valuex.GetType() == typeof(int))
                         {
                             core += (int)valuex;
                         }
                         else
                         {

                             core += "\"" + valuex.ToString() + "\"";

                         }

                     }
                     html += core;

                     html += "}";

                 }

                html += "}});";
                //Returning Script
                return html;
        }

        private static string fixScript(string script, bool withTags)
        {
            if (withTags)
            {

                script = "<script type=\"text/javascript\">" +
                            "$(function(){" +
                            script +
                            "});" +
                         "</script>";

            }

            return script;
        }

        private static string simID(string elID)
        {
            if (elID == null)
            {

                string id = Guid.NewGuid().ToString();

                CryptoServices Cry = new CryptoServices();

                id = Cry.ConvertToSHA1(id);

                id += "_JSTREE";

                elID = id;

            }
            lastID = elID;
            return elID;
        }

        #endregion Methods
    }

    public class JsTreeJsonResult : JsonResult
    {
        #region Fields

        private List<object> nodes = new List<object>();

        #endregion Fields

        #region Methods

        public void addNode(string id, string relation, string text, bool stateOpen=false)
        {
            this.nodes.Add(new
            {
                attr = new { id = id, rel = relation },
                data = text,
                state = (stateOpen ? "" : "closed")
            });
        }

        public override void ExecuteResult(ControllerContext context)
        {
            this.Data = this.nodes;

            base.ExecuteResult(context);
        }

        #endregion Methods
    }
}