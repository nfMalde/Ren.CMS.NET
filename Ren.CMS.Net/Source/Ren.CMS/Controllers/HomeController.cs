namespace Ren.CMS.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.CORE.Extras;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.Models;

    public class HomeController : Controller
    {
        #region Methods

        public ActionResult About()
        {
            return View();
        }

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
         
            max = maxX.toInt();

            Ren.CMS.Content.ContentManagement.GetContent News = new Ren.CMS.Content.ContentManagement.GetContent(new string[] {
            "eNews",
            "eArticle"

            },null,
            "{prefix}Content.cDate","DESC",false,page,max);

            List<Ren.CMS.Content.nContent> N = News.getList();
            Pagination.nPagingCollection pages = new Pagination.nPagingCollection(News.TotalRows, max);
            ViewData["TotalRows"] = News.TotalRows;
            ViewData["News"] = N;
            ViewData["Show"] = N.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;

            return View();
        }

        #endregion Methods
    }
}