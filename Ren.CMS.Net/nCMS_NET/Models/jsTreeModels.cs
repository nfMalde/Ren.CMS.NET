using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ren.CMS.Models.jsTreeModels
{
   
    public class jsTreeInternalUrl
    {
        private string url = "/";
        public jsTreeInternalUrl(string Action, string Controller)
        {
        
        
       url+=Controller + "/"+ Action;    
       
        }
       

        public string Url{get{return (this.url);}}


    }

    public class jsTreeAjax
    {

        public string dataType = "json_data";

        public string url { get; set; }

        public bool UrlIsJavaScript = false;


        public string method = "POST";

        public Dictionary<string, string> additionalRequestParameters = new Dictionary<string,string>();


    
    }
    public class jsTreeType
    {

        public string name { get; set; }
        
        public string[] valid_children { get; set; }

        public string icon { get; set; }

       
    }
    public class jsTreeTypes
    {
        private int p_max_depth = -2;
        private int p_max_children = -2;



        public int max_depth {
            get { return this.p_max_depth; }

            set { this.p_max_depth = value; }
        
        }

        public int max_children
        {

            get
            {
                return this.p_max_children;


            }
            set
            { this.p_max_children = value; }
        }

        public string[] valid_children { get; set; }



        public List<jsTreeType> types = new List<jsTreeType>();
    
    
    
     
        }

    public class jsTreePlugins
    {
        private List<string> temp = new List<string>() { "themes", "html_data", "ui", "crrm", "hotkeys" };


        private bool pluginExists(string newPlugin) {

            foreach (string plugin in this.temp)
            {

                if (newPlugin == plugin) return true;
            }
            return false;
        }

        public void Add(string[] pluginNames)
        {

            this.temp.AddRange(pluginNames);
        
        }
        public void Add(string pluginName)
        {

            if (!this.pluginExists(pluginName)) this.temp.Add(pluginName);


        
        }


        public void Remove(string pluginName)
        {
            this.temp.Remove(pluginName);


        }
        
        public List<string> Plugins{get{return this.temp; }}

        public void ClearPlugins()
        {

            this.temp.Clear();
        
        }

        public bool PluginAdded(string pluginName)
        { return this.pluginExists(pluginName); }
            
    
    
    }

    public class jsTreeSettings
    {

        public jsTreePlugins plugins = new jsTreePlugins();





        public jsTreeSettings() {
        
          
        
        }

        public jsTreeAjax ajax_read = new jsTreeAjax();

        public jsTreeAjax ajax_search = new jsTreeAjax();

        public jsTreeAjax ajax_delete = new jsTreeAjax();

        public jsTreeAjax ajax_edit = new jsTreeAjax();




        public jsTreeTypes types = new jsTreeTypes();

        public Dictionary<string, object> ui = new Dictionary<string, object>();

        public Dictionary<string, object> core = new Dictionary<string, object>();

    }
}