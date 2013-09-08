namespace Ren.CMS.Article
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS;
    using Ren.CMS.Article.Models;
    using Ren.CMS.Content;
    using Ren.CMS.CORE.Extras;
    using Ren.CMS.CORE.Permissions;
    using Ren.CMS.CORE.SettingsHelper;
    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.MemberShip;
    using Ren.CMS.nModules;
    using Ren.CMS.Pagination;

    public class ArticleController : Controller
    {
        #region Methods

        public ActionResult Archive(int id=1, string category = "", string subcategory = "")
        {
            int page = id;
            if (id < 1) page = 1;

            Ren.CMS.CORE.SettingsHelper.GlobalSettingsHelper G = new Ren.CMS.CORE.SettingsHelper.GlobalSettingsHelper();
            int max = 0;
            if (String.IsNullOrEmpty(G.Read("GLOBAL_NEWS_MAX_ENTRIES")))
            {

                G.Write("GLOBAL_NEWS_MAX_ENTRIES", "10");

            }
            max = Convert.ToInt32(G.Read("GLOBAL_NEWS_MAX_ENTRIES"));

            Ren.CMS.Content.ContentManagement.GetContent News = new Ren.CMS.Content.ContentManagement.GetContent(new string[] {
            "eArticle"

            }, null, "{prefix}Content.cDate", "DESC", false, page, max);

            LocationBar Bar = new LocationBar(this.ControllerContext);
            Bar.AddLocation("NetworkFreaks.de", "/Home/Index");
            Bar.AddLocation("Artikel", "/Article/Archive",true);

            Bar.Render();
            List<Ren.CMS.Content.nContent> N = News.getList();
            nPagingCollection pages = new nPagingCollection(News.TotalRows, max);
            ViewData["TotalRows"] = News.TotalRows;
            ViewData["News"] = N;
            ViewData["Show"] = N.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;
            ViewData["totalRating"] = new Ren.CMS.Extensions.ArticleExt.LoadTotalRating(id).toDecimal();
            ViewData["ratings"] = new Ren.CMS.Extensions.ArticleExt.initRatings(id).getRatings();

            return View();
        }

        public ActionResult Category(string category, int page = 1, string subname = null)
        {
            ContentManagement.GetContent GetC = new ContentManagement.GetContent(new string[]{"eArticle"},category,"{prefix}Content.cDate","DESC",false,page,20);

            string c = "";
            List<nContent> Li = GetC.getList();
            if (Li.Count > 0) c = Li[0].CategoryName;
            else return RedirectToAction("Archive", "Article");

            LocationBar Bar = new LocationBar(this.ControllerContext);
            Bar.AddLocation("NetworkFreaks.de", "/Home/Index");
            Bar.AddLocation("Artikel", "/Article/Archive");
            Bar.AddLocation(category, "/Article/Category/" + Li[0].CategoryName, true);

            Bar.Render();
            nPagingCollection pages = new nPagingCollection(GetC.TotalRows, 10);
            ViewData["TotalRows"] = GetC.TotalRows;
            ViewBag.CatName = c;
            ViewData["Show"] = Li.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;
            ViewData["Entries"] = Li;

            return View();
        }

        [HttpPost]
        public ActionResult Comment(int id, ArticleComment c, bool captchaValid, string returnUrl)
        {
            if (ModelState.IsValid && captchaValid)
            {

                nProvider.CurrentUser IUSR = new nProvider.CurrentUser();

                if (!captchaValid) return View("CaptchaError");

                if (id != null && id > 0)
                {

                    string text = c.text.ToString();
                    GlobalSettingsHelper GS2 = new GlobalSettingsHelper();

                    //Encode HTML =
                    text = HttpUtility.HtmlEncode(text);

                    string nickname = c.nickname;

                    if (GS2.empty(nickname)) nickname = IUSR.nUser.UserName;
                    if (IUSR.isGuest()) nickname = IUSR.nUser.UserName;
                    if (!GS2.empty(text))
                    {
                        Ren.CMS.CORE.SettingsHelper.GlobalSettingsHelper GS = new Ren.CMS.CORE.SettingsHelper.GlobalSettingsHelper();

                        Ren.CMS.Content.ContentManagement MNG = new Ren.CMS.Content.ContentManagement();

                        Ren.CMS.Content.ContentManagement.GetContent GCo = new Ren.CMS.Content.ContentManagement.GetContent(id);
                        List<Ren.CMS.Content.nContent> CO = GCo.getList();

                        if (CO.Count > 0)
                        {

                            Ren.CMS.Content.nContent E = new Ren.CMS.Content.nContent(0,
                "Comment",
                "eArticleComment",
                CO[0].CategoryID,

                CO[0].CategoryName, IUSR.nUser.ProviderUserKey, IUSR.nUser.UserName, false, "COMMENT_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Millisecond, 0, DateTime.Now, "", "", "",
                text, id, nickname
                );

                            if (MNG.InsertContent(E)) return RedirectToAction("Show/" + CO[0].SEOName);

                        }
                    }
                }
            }

            return View(c);
        }

        [HttpPost]
        public ActionResult GalleryAjaxadapter()
        {
            string atype = this.Request.Form["atype"];
            string newsID = this.Request.Form["newsID"];

            string startIndex = this.Request.Form["startIndex"];
            int inewsID = 0;

            int.TryParse(newsID, out inewsID);

            if (inewsID == 0) return Content("");

            if (String.IsNullOrEmpty(atype)) return Content("");
            Ren.CMS.Content.ContentManagement.GetContent G = new Ren.CMS.Content.ContentManagement.GetContent(inewsID);
            if (G.getList().Count == 0) return Content("");
            Ren.CMS.Content.nContent C = G.getList()[0];
            // Page = 2; max = 10; Entries = 30;
            // PageStart = (2 * 10) -10;
            // PageEnd = (2*10)
            //
            GlobalSettingsHelper GS = new GlobalSettingsHelper();

            int sizeImg = Convert.ToInt32(GS.Read("GLOBAL_NEWS_MAX_GALLERY_IMAGES"));
            int sizeVids = Convert.ToInt32(GS.Read("GLOBAL_NEWS_MAX_GALLERY_VIDEOS"));

            switch (atype)
            {

                case "images":
                    List<Ren.CMS.Content.nContent.nAttachment> Gallery = C.Attachments("image", "gallery");
                    int startI = 0;
                    int.TryParse(startIndex, out startI);
                    int pageSize =
                    startI = startI + sizeImg;
                    if (Gallery.Count > startI)
                    {

                        Gallery.RemoveRange(0, startI);

                    }
                    else {

                        return Content("");

                    }

                    if (Gallery.Count > 0)
                    {
                        ViewBag.Type = "IMG";
                        ViewData["News"] = C;
                        ViewData["Gallery"] = Gallery;

                        return View();
                    }
                    break;

                case "videos":

                    List<Ren.CMS.Content.nContent.nAttachment> Videos = C.Attachments("flv", "video");
                    int startII = 0;
                    int.TryParse(startIndex, out startII);

                    startI = startII + sizeVids;
                    if (Videos.Count > startI)
                    {

                        Videos.RemoveRange(0, startI);

                    }
                    else {

                        return Content("");

                    }

                    if (Videos.Count > 0)
                    {
                        ViewBag.Type = "VID";
                        ViewData["News"] = C;
                        ViewData["Gallery"] = Videos;

                        return View();
                    }

                    break;

                default:

                    return Content("");

            }
            return Content("");
        }

        //
        // GET: /News/
        public ActionResult Index()
        {
            return RedirectToAction("Archive");
        }

        [HttpGet]
        public ActionResult Show(int id, string title= "", int page = 1)
        {
            if (page < 1) page = 1;
            List<Ren.CMS.Content.nContent> Entry = new List<Ren.CMS.Content.nContent>();
            List<Ren.CMS.Content.nContent> Comments = new List<Ren.CMS.Content.nContent>();

            if (id == 0) return RedirectToActionPermanent("Code/404", "Error");
            else {

                int idd = id;

                if (idd == -1) return RedirectToAction("Archive");
                Ren.CMS.Content.ContentManagement.GetContent G = new Ren.CMS.Content.ContentManagement.GetContent(idd);
                Ren.CMS.Content.ContentManagement.GetContent C = new Ren.CMS.Content.ContentManagement.GetContent(new string[] { "eArticleComment" },null,"{prefix}Content.cDate","DESC",false,page,10,idd);

                 Entry = G.getList();
                Comments = C.getList();
                if (Entry.Count == 0) RedirectToActionPermanent("Code/404", "Error");
                Entry[0].GenerateLink();
                if (Entry[0].ContentType != "eNews") RedirectToActionPermanent(Entry[0].TargetAction, Entry[0].TargetController);
                nPagingCollection pages = new nPagingCollection(C.TotalRows, 10);
                Ren.CMS.Extensions.ArticleExt.LoadTotalRating TOTAL = new Extensions.ArticleExt.LoadTotalRating(id);
                LocationBar Bar = new LocationBar(this.ControllerContext);
                Bar.AddLocation("NetworkFreaks.de", "/Home/Index");
                Bar.AddLocation("Artikel", "/Article/Archive");
                Bar.AddLocation(Entry[0].Title, "",true);

                Bar.Render();
                ViewData["totalRating"] = TOTAL.toDecimal();
                ViewData["TotalRows"] = C.TotalRows;
                ViewData["Show"] = Comments.Count;
                ViewData["Pages"] = pages;
                ViewData["Page"] = page;
            }

            ViewData["Entry"] = Entry[0];
            ViewData["Comments"] = Comments;

            return View();
        }

        public ActionResult Tag(string tgn, int page = 1)
        {
            ContentManagement.GetContent GetC = new ContentManagement.GetContent();
            string[] t = new string[1];
            t[0] = "eArticle";

            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            string realTag = tgn;
            string query = "SELECT COUNT(*) as tagCount, tagName FROM " + (new Ren.CMS.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags WHERE tagNameSEO=@tagName AND contentType=@ct AND enableBrowsing=1 GROUP BY tagName";

            nSqlParameterCollection N = new nSqlParameterCollection();

            N.Add("@tagName", tgn);
            N.Add("@ct", "eArticle");
            SqlDataReader R = SQL.SysReader(query, N);
            if (!R.HasRows) return RedirectToAction("Archive", "Article");
            R.Read();

            int count = 0;

            if (R["tagCount"] != DBNull.Value) {

                count = (int)R["tagCount"];
                ViewBag.TagName = (string)R["tagName"];
                realTag = (string)R["tagName"];

            }
            R.Close();
            SQL.SysDisconnect();
            if (count == 0) return RedirectToAction("Archive");

            GetC.GetContentByTag(realTag, t,null,"{prefix}Content.cDate","DESC",false,page,20);
            LocationBar Bar = new LocationBar(this.ControllerContext);
            Bar.AddLocation("NetworkFreaks.de", "/Home/Index");
            Bar.AddLocation("Artikel", "/Article/Archive");
            Bar.AddLocation("Nach Tag: <u>" + realTag + "</u>", "/Article/Tag/" + tgn, true);
            Bar.Render();
            List<nContent> Li = GetC.getList();

            nPagingCollection pages = new nPagingCollection(GetC.TotalRows, 10);
            ViewData["TotalRows"] = GetC.TotalRows;
            ViewData["Show"] = Li.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;
            ViewData["Entries"] = Li;

            return View();
        }

        #endregion Methods
    }
}