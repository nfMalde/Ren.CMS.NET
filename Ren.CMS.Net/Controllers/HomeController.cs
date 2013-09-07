using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using nfCMS_NET.Models;
using nfCMS_NET.CORE.Extras;
using nfCMS_NET.CORE.SqlHelper;
using nfCMS_NET.CORE.Settings;
using System.Data.SqlClient;
namespace nfCMS_NET.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/Index/1
        public ActionResult Index(int id=1)
        {
            GlobalSettings G = new GlobalSettings();
            LocationBar Bar = new LocationBar(this.ControllerContext);
            Bar.AddLocation("NetworkFreaks.de", "/Home/Index", false);
            Bar.AddLocation("Übersicht", "/Home/Index", true);
            Bar.Render();
            nSetting maxX = G.getSetting("GLOBAL_INDEX_MAX_ENTRIES");
            string INDEX_MAX = "";

            if (maxX.Value == null)
            {

                INDEX_MAX = "";
            }
            else {

                INDEX_MAX = maxX.Value.ToString();
            
            }

            int page = id;
            if (id<1) page = 1;



     
            int max = 0;
            if (String.IsNullOrEmpty(INDEX_MAX)) {
                nSetting N1 = new nSetting();
                N1.Value = 10;
                N1.Name = "GLOBAL_INDEX_MAX_ENTRIES";
                N1.Label = "Maximale Einträge auf der Startseite";
                N1.Description = "Hier können Sie die Maximalanzahl der Beiträge auf der Startseite einstellen";
                N1.SettingType = new nSettingType().SettingString;
                N1.ValueType = new nValueType().ValueString;
                N1.PermissionFrontend = null;
                N1.DefaultValue = 10;
                N1.PermissionBackend = "USR_IS_ADMIN";
                N1.CategoryName = "Contents";
                bool ok = G.AddSetting(N1);
                if(ok) maxX =N1;
               
            
            }

            max = maxX.toInt();
           
            

            nfCMS_NET.Content.ContentManagement.GetContent News = new nfCMS_NET.Content.ContentManagement.GetContent(new string[] { 
    "eNews",
    "eArticle"
    
    },null,null,"{prefix}Content.cDate","DESC",false,page,max);

             
            List<nfCMS_NET.Content.nContent> N = News.getList();
            Pagination.nPagingCollection pages = new Pagination.nPagingCollection(News.TotalRows, max); 
            ViewData["TotalRows"] = News.TotalRows;
            ViewData["News"] = N;
            ViewData["Show"] = N.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
