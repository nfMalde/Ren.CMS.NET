using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nCMS_NET.nModules;
using nCMS_NET.News.Models;
using nCMS_NET.CORE.SqlHelper;
using System.Data.SqlClient;
using nCMS_NET;
using nCMS_NET.Pagination;
using nCMS_NET.MemberShip;
using nCMS_NET.CORE.Permissions;
using nCMS_NET.CORE.Captcha;
using nCMS_NET.CORE.GlobalSettings;
using nCMS_NET.Content;
namespace nCMS_NET.News
{
    public class ArticleController : Controller
    {

        
        public ActionResult Tag(string tgn, int page = 1) {
             
         
            ContentManagement.GetContent GetC = new ContentManagement.GetContent();
            string[] t = new string[1];
            t[0] = "eNews";

            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            string realTag = tgn;
            string query = "SELECT COUNT(*) as tagCount, tagName FROM " + (new nCMS_NET.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags WHERE tagNameSEO=@tagName AND contentType=@ct AND enableBrowsing=1 GROUP BY tagName";

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


            GetC.GetContentByTag(realTag, t,null,null,"{prefix}Content.cDate","DESC",false,page,20);


          
            List<nContent> Li = GetC.getList();

            nPagingCollection pages = new nPagingCollection(GetC.TotalRows, 10);
            ViewData["TotalRows"] = GetC.TotalRows;
            ViewData["Show"] = Li.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;
            ViewData["Entries"] = Li;
           



            return View();
        
        }

        [HttpPost]
        [CaptchaValidator]
        public ActionResult Comment(int id, NewsComment c, bool captchaValid, string returnUrl)
        {
            if (ModelState.IsValid && captchaValid)
            {
                nPermissions Permission = new nPermissions();
                nProvider.CurrentUser IUSR = new nProvider.CurrentUser();


                if (!captchaValid) return View("CaptchaError");

                if (id != null && id > 0)
                {

                    string text = c.text.ToString();
                    GlobalSettings GS2 = new GlobalSettings();

                    //Encode HTML = 
                    text = HttpUtility.HtmlEncode(text);

                    string nickname = c.nickname;

                    if (GS2.empty(nickname)) nickname = IUSR.nUser.UserName;
                    if (IUSR.isGuest()) nickname = IUSR.nUser.UserName;
                    if (!GS2.empty(text))
                    {
                        nCMS_NET.CORE.GlobalSettings.GlobalSettings GS = new nCMS_NET.CORE.GlobalSettings.GlobalSettings();

                        nCMS_NET.Content.ContentManagement MNG = new nCMS_NET.Content.ContentManagement();


                        nCMS_NET.Content.ContentManagement.GetContent GCo = new nCMS_NET.Content.ContentManagement.GetContent(id);
                        List<nCMS_NET.Content.nContent> CO = GCo.getList();




                        if (CO.Count > 0)
                        {


                            nCMS_NET.Content.nContent E = new nCMS_NET.Content.nContent(0,
                "Comment",
                "eNewsComment",
                CO[0].CategoryID,
                CO[0].SubCategoryID,
                CO[0].CategoryName, CO[0].SubCategoryName, IUSR.nUser.ProviderUserKey, IUSR.nUser.UserName, false, "COMMENT_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Millisecond, 0, DateTime.Now, "", "", "",
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
        public ActionResult GalleryAjaxadapter() {
            string atype = this.Request.Form["atype"];
            string newsID = this.Request.Form["newsID"];
        

            string startIndex = this.Request.Form["startIndex"];
            int inewsID = 0;
         

            int.TryParse(newsID, out inewsID);
          
            if (inewsID == 0) return Content("");
          
            if (String.IsNullOrEmpty(atype)) return Content("");
            nCMS_NET.Content.ContentManagement.GetContent G = new nCMS_NET.Content.ContentManagement.GetContent(inewsID);
            if (G.getList().Count == 0) return Content("");
            nCMS_NET.Content.nContent C = G.getList()[0];
            // Page = 2; max = 10; Entries = 30;
            // PageStart = (2 * 10) -10;
            // PageEnd = (2*10)
            //
            GlobalSettings GS = new GlobalSettings();

            int sizeImg = Convert.ToInt32(GS.Read("GLOBAL_NEWS_MAX_GALLERY_IMAGES"));
            int sizeVids = Convert.ToInt32(GS.Read("GLOBAL_NEWS_MAX_GALLERY_VIDEOS"));


            switch (atype)
            { 
            
                case "images":
                    List<nCMS_NET.Content.nContent.nAttachment> Gallery = C.Attachments("image", "gallery");
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

                    List<nCMS_NET.Content.nContent.nAttachment> Videos = C.Attachments("flv", "video");
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
        public ActionResult SubCategory(string category, string subname, int page = 1) {







            ContentManagement.GetContent GetC = new ContentManagement.GetContent(new string[] { "eNews" }, category, subname, "{prefix}Content.cDate", "DESC", false, page, 20);







            List<nContent> Li = GetC.getList();
            string c = "";

            if (Li.Count > 0) c = Li[0].SubCategoryName;
            else return RedirectToAction("Archive", "News");
            nPagingCollection pages = new nPagingCollection(GetC.TotalRows, 10);
            ViewData["TotalRows"] = GetC.TotalRows;
            ViewData["Show"] = Li.Count;
            ViewBag.SubCatName = c;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;
            ViewData["Entries"] = Li;




            return View();
        
        
        
        }
        public ActionResult Category(string category, int page = 1, string subname = null) {




            ContentManagement.GetContent GetC = new ContentManagement.GetContent(new string[]{"eArticle"},category,subname,"{prefix}Content.cDate","DESC",false,page,20);






            string c = "";
            List<nContent> Li = GetC.getList();
            if (Li.Count > 0) c = Li[0].CategoryName;
            else return RedirectToAction("Archive", "News");
            nPagingCollection pages = new nPagingCollection(GetC.TotalRows, 10);
            ViewData["TotalRows"] = GetC.TotalRows;
            ViewBag.CatName = c;
            ViewData["Show"] = Li.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;
            ViewData["Entries"] = Li;




            return View();
        
        
        
        
        
        }


        [HttpGet]
        public ActionResult Show(int id, string title= "", int page = 1) {
             

           

          
           
            if (page < 1) page = 1;
            List<nCMS_NET.Content.nContent> Entry = new List<nCMS_NET.Content.nContent>();
            List<nCMS_NET.Content.nContent> Comments = new List<nCMS_NET.Content.nContent>();

            if (id == 0) return RedirectToActionPermanent("Code/404", "Error");
            else {


                int idd = id;
                
                if (idd == -1) return RedirectToAction("Archive");
                nCMS_NET.Content.ContentManagement.GetContent G = new nCMS_NET.Content.ContentManagement.GetContent(idd);
                nCMS_NET.Content.ContentManagement.GetContent C = new nCMS_NET.Content.ContentManagement.GetContent(new string[] { "eNewsComment" },null,null,"{prefix}Content.cDate","DESC",false,page,10,idd);
                                    
                 Entry = G.getList();
                Comments = C.getList();
                if (Entry.Count == 0) RedirectToActionPermanent("Code/404", "Error");
                Entry[0].GenerateLink();
                if (Entry[0].ContentType != "eNews") RedirectToActionPermanent(Entry[0].TargetAction, Entry[0].TargetController);
                nPagingCollection pages = new nPagingCollection(C.TotalRows, 10);
                ViewData["TotalRows"] = C.TotalRows;
                ViewData["Show"] = Comments.Count;
                ViewData["Pages"] = pages;
                ViewData["Page"] = page;
            }

  
            ViewData["Entry"] = Entry[0];
            ViewData["Comments"] = Comments;
           
            return View();
        }


        //
        // GET: /News/
        public ActionResult Index()
        {



            return RedirectToAction("Archive");
        }
        public ActionResult Archive(int id=1, string category = "", string subcategory = "")
        {



            int page = id;
            if (id < 1) page = 1;



            nCMS_NET.CORE.GlobalSettings.GlobalSettings G = new nCMS_NET.CORE.GlobalSettings.GlobalSettings();
            int max = 0;
            if (String.IsNullOrEmpty(G.Read("GLOBAL_NEWS_MAX_ENTRIES")))
            {

                G.Write("GLOBAL_NEWS_MAX_ENTRIES", "10");

            }
            max = Convert.ToInt32(G.Read("GLOBAL_NEWS_MAX_ENTRIES"));




            nCMS_NET.Content.ContentManagement.GetContent News = new nCMS_NET.Content.ContentManagement.GetContent(new string[] { 
    "eArticle"
    
    }, null, null, "{prefix}Content.cDate", "DESC", false, page, max);


            List<nCMS_NET.Content.nContent> N = News.getList();
            nPagingCollection pages = new nPagingCollection(News.TotalRows, max);
            ViewData["TotalRows"] = News.TotalRows;
            ViewData["News"] = N;
            ViewData["Show"] = N.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;

     
 
 
            return View();
}
    }
}
