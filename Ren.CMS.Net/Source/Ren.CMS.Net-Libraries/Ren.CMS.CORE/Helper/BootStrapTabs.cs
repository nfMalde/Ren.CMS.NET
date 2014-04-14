using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace Ren.CMS.CORE.Helper
{
    public class BootStrapTab
    {
        public string ID { get; set; }
        public string Caption { get; set; }
        public string PartialView { get; set; }
        public bool Active { get; set; }
        public object Model { get; set; }
        
        public ViewDataDictionary ViewData { get; set; }
    }


    public class BottStrapTabsModel
    {

        public List<BootStrapTab> Tabs { get; set; }
        public string Title { get; set; }
        public string ID { get; set; }
       
    }

    public class BootStrapTabs
    {
        private HtmlHelper _helper = null;
        public BootStrapTabs(HtmlHelper helper)
        {
           
            this._helper = helper;
        }
        private List<BootStrapTab> _tabs = new List<BootStrapTab>();
        private string _title = null;
        private string _id = null;

 
      

        public BootStrapTabs AddTab(string ID, string Caption, string PartialView, object model = null, bool active = false, ViewDataDictionary viewData = null)
        {

            if (active && this._tabs.Any(e => e.Active))
            {

                var i = this._tabs.IndexOf(this._tabs.Where(e => e.Active).FirstOrDefault());
                var el = this._tabs.Where(e => e.Active).FirstOrDefault();
                el.Active = false;
                this._tabs[i] = el;
            }

            this._tabs.Add(new BootStrapTab() { ID = ID, Caption = Caption, PartialView = PartialView, Model = model, Active = active, ViewData = viewData });
            return this;
        }

        public BootStrapTabs SetTitle(string Title)
        {

            this._title = Title;

            return this;

        }

        public BootStrapTabs SetID(string ID) 
        {
            this._id = ID;
            
            return this;
        
        
        }


        public string Render()
        {
            if (String.IsNullOrEmpty(this._id))
                this._id = "tabs";

            this._helper.RenderPartial("_SharedBootstrapTabs", new BottStrapTabsModel() { 
                ID = this._id,
                Title = this._title,
                Tabs = this._tabs
            
            });

            return "";
        }


       
    
    
    
    }



    public static class BootStrapTabsHelper
    { 
        public static BootStrapTabs bootStrapTabs (this HtmlHelper helper)
        {


            return new BootStrapTabs(helper);
        }
    
    }
}
