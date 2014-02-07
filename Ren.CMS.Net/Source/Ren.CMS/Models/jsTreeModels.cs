namespace Ren.CMS.Models.jsTreeModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class jsTreeAjax
    {
        #region Fields

        public Dictionary<string, string> additionalRequestParameters = new Dictionary<string,string>();
        public string dataType = "json_data";
        public string method = "POST";
        public bool UrlIsJavaScript = false;

        #endregion Fields

        #region Properties

        public string url
        {
            get; set;
        }

        #endregion Properties
    }

    public class jsTreeInternalUrl
    {
        #region Fields

        private string url = "/";

        #endregion Fields

        #region Constructors

        public jsTreeInternalUrl(string Action, string Controller)
        {
            url+=Controller + "/"+ Action;
        }

        #endregion Constructors

        #region Properties

        public string Url
        {
            get{return (this.url);}
        }

        #endregion Properties
    }

    public class jsTreePlugins
    {
        #region Fields

        private List<string> temp = new List<string>() { "themes", "html_data", "ui", "crrm", "hotkeys" };

        #endregion Fields

        #region Properties

        public List<string> Plugins
        {
            get{return this.temp; }
        }

        #endregion Properties

        #region Methods

        public void Add(string[] pluginNames)
        {
            this.temp.AddRange(pluginNames);
        }

        public void Add(string pluginName)
        {
            if (!this.pluginExists(pluginName)) this.temp.Add(pluginName);
        }

        public void ClearPlugins()
        {
            this.temp.Clear();
        }

        public bool PluginAdded(string pluginName)
        {
            return this.pluginExists(pluginName);
        }

        public void Remove(string pluginName)
        {
            this.temp.Remove(pluginName);
        }

        private bool pluginExists(string newPlugin)
        {
            foreach (string plugin in this.temp)
            {

                if (newPlugin == plugin) return true;
            }
            return false;
        }

        #endregion Methods
    }

    public class jsTreeSettings
    {
        #region Fields

        public jsTreeAjax ajax_delete = new jsTreeAjax();
        public jsTreeAjax ajax_edit = new jsTreeAjax();
        public jsTreeAjax ajax_read = new jsTreeAjax();
        public jsTreeAjax ajax_search = new jsTreeAjax();
        public Dictionary<string, object> core = new Dictionary<string, object>();
        public jsTreePlugins plugins = new jsTreePlugins();
        public jsTreeTypes types = new jsTreeTypes();
        public Dictionary<string, object> ui = new Dictionary<string, object>();

        #endregion Fields

        #region Constructors

        public jsTreeSettings()
        {
        }

        #endregion Constructors
    }

    public class jsTreeType
    {
        #region Properties

        public string icon
        {
            get; set;
        }

        public string name
        {
            get; set;
        }

        public string[] valid_children
        {
            get; set;
        }

        #endregion Properties
    }

    public class jsTreeTypes
    {
        #region Fields

        public List<jsTreeType> types = new List<jsTreeType>();

        private int p_max_children = -2;
        private int p_max_depth = -2;

        #endregion Fields

        #region Properties

        public int max_children
        {
            get
            {
                return this.p_max_children;

            }
            set
            { this.p_max_children = value; }
        }

        public int max_depth
        {
            get { return this.p_max_depth; }

            set { this.p_max_depth = value; }
        }

        public string[] valid_children
        {
            get; set;
        }

        #endregion Properties
    }
}