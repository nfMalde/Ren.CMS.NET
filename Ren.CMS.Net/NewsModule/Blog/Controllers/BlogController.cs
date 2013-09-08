namespace Ren.CMS.Blog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;

    using BlogModule.Blog.Helpers;

    using Recaptcha;

    using Ren.CMS;
    using Ren.CMS.Blog.Helpers;
    using Ren.CMS.Blog.Models;
    using Ren.CMS.Content;
    using Ren.CMS.ContentHelpers.Blog;
    using Ren.CMS.CORE.Permissions;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.CORE.SettingsHelper;
    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.CORE.ThisApplication;
    using Ren.CMS.MemberShip;
    using Ren.CMS.nModules;
    using Ren.CMS.Pagination;

    public class BlogController : Controller
    {
        #region Properties

        public string ContentType
        {
            get
            {
                string routingCT = BlogTypeRouteData;
                routingCT = routingCT.ToLower();
                var vals = Enum.GetValues(typeof(BlogTypeEnum)).Cast<BlogTypeEnum>();

                foreach (BlogTypeEnum exc in vals)
                {
                    string v = exc.ToString().ToLower();
                    if ( v == routingCT)
                    {
                        routingCT = BlogModule.Blog.Helpers.EnumHelper<BlogTypeEnum>.GetEnumDescription(v);
                    }

                }

                return routingCT;
            }
        }

        private string BlogTypeRouteData
        {
            get{
                string routingCT = "all";

                if(this.RouteData.Values["contentType"]  != null)
                  routingCT = (string)this.RouteData.Values["contentType"]  ?? "all";

                return routingCT;
            }
        }

        #endregion Properties

        #region Methods

        public ActionResult Archive(int id=1)
        {
            int count = 0;
            Dictionary<DateTime, List<nContent>> cList = new Dictionary<DateTime, List<nContent>>();

            DateTime DT = DateTime.Now;
            int idcount = 0;
            int Total = 0;
            int OnPage = 0;
            for (int x = 1; x <= id; x++)
            {

                ContentManagement.GetContent Getter = new ContentManagement.GetContent(acontenttypes: new string[] { "eNews" }, languages: new string[] { Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage }, pageSize: 50, pageIndex: x);
                var l = Getter.getList();

                if (cList.Keys.Where(e => e == DT).Count() > 0)
                {
                    cList[DT].AddRange(l);

                }
                else
                    cList.Add(DT, l);

                count = count + l.Count ;

                if (count == 50)
                {
                    DT = l.Last().CreationDate;
                    count = 0;

                }
                Total = Total + Getter.TotalRows;
                OnPage = OnPage + l.Count;
            };

            //Split Every 30 Rows

            NewsArchive archive = new NewsArchive();
            archive.News = cList;
            archive.RowsOnPage = OnPage;
            archive.TotalRows = Total;
            archive.Page = id;

            return View(archive);
        }

        [HttpPost]
        public JsonResult ArchiveAjax(int page = 1)
        {
            ContentManagement.GetContent AjaxContent = new ContentManagement.GetContent(acontenttypes: new string[] {ContentType }, languages: new string[] { Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage }, pageSize: 50, pageIndex: page);
            var list = AjaxContent.getList();
            if(list.Count <= 0)
                return Json(new { Contents = new List<object>(), TotalRows = AjaxContent.TotalRows, Rows = list.Count });

            var key = "List_" + list.First().CreationDate.ToString("ddMMyyyyHHmm");
            var keyText = list.First().CreationDate.ToString("dd.MM.yyyy HH:mm");
            list.ForEach(e => e.GenerateLink());
            var listC = new List<object>();
            list.ForEach(e => listC.Add(new { DateString = e.CreationDate.ToString("dd.MM.yyyy HH:mm"), Row = e }));

            List<object> Contents = new List<object>();
            Contents.Add(new {

                Key = key,
                KeyText = keyText,
                List = listC

            });

            return Json(new { Contents = Contents, TotalRows = AjaxContent.TotalRows, Rows = list.Count });
        }

        public ActionResult Category(string category, int page = 1, string subname = null)
        {
            ContentManagement.GetContent GetC = new ContentManagement.GetContent(new string[]{this.ContentType},category,"{prefix}Content.cDate","DESC",false,page,20);

            string c = "";
            List<nContent> Li = GetC.getList();
            if (Li.Count > 0) c = Li[0].CategoryName;
            else return RedirectToAction( this.BlogTypeRouteData + "/Archive", "News");
            nPagingCollection pages = new nPagingCollection(GetC.TotalRows, 10);
            ViewData["TotalRows"] = GetC.TotalRows;
            ViewBag.CatName = c;
            ViewData["Show"] = Li.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;
            ViewData["Entries"] = Li;

            return View();
        }

        // bool captchaValid, string returnUrl
        public ActionResult Comments(int id)
        {
            //Get News

            ContentManagement.GetContent Get = new ContentManagement.GetContent(id: id);

            var newsl = Get.getList();

            if (newsl.Count == 0 || newsl.First().ContentType !=ContentType)
            {
                return RedirectToAction(BlogTypeRouteData + "/Archive", "Blog");

            }
            var news = newsl.First();

            ContentManagement.GetContent Comments = new ContentManagement.GetContent(acontenttypes: new string[] { "eComment" }, contentRef: news.ID);

            NewsDetail DetailView = new NewsDetail();
            DetailView.Comments = Comments.getList();
            news.GenerateLink();
            DetailView.NewsEntry = news;
            DetailView.mode = "all";

            return View(DetailView);
        }

        [HttpPost]
        public JsonResult GetAnswerInfo(int id)
        {
            ContentManagement.GetContent Get = new ContentManagement.GetContent(id: id);

            var entry = Get.getList();

            if (entry.Count == 0)
            {

                return Json(null);

            }
               var entryOne = entry.First();

            return Json(new { CreatorName = (entryOne.CreatorSpecialName != "" ? entryOne.CreatorSpecialName : entryOne.CreatorName), ID = entryOne.ID });
        }

        //
        // GET: /News/
        public ActionResult Index()
        {
            return RedirectToAction(BlogTypeRouteData + "/Archive");
        }

        public ActionResult PartialCommentAnswers(int NewsID, int CommentID, int Page = 1)
        {
            var newsEntry = new ContentManagement.GetContent(id: NewsID).getList();
            if (newsEntry.Count == 0)
            {

                return Content(String.Empty);
            }

            NewsCommentAnswerView Answers = new NewsCommentAnswerView();
            Answers.MainCommentID = CommentID;
            Answers.NewsEntry = newsEntry.First();
            Answers.Page = Page;

            return PartialView("_CommentAnswers", Answers);
        }

        [HttpGet]
        public ActionResult Show(int id = 0, string title= "", int page = 1)
        {
            var model = this.getDetailViewModel(id, page);
            if (model == null)
            {
                return RedirectToActionPermanent(BlogTypeRouteData + "/Archive", "Blog");

            }

            return View(model);
        }

        [HttpPost]
        [RecaptchaControlMvc.CaptchaValidator]
        public ActionResult Show(int id, string title, NewsComment Model, bool captchaValid, string captchaErrorMessage, object page = null)
        {
            var model = this.getDetailViewModel(id);
            ModelState.ValidateCaptcha(captchaValid, captchaErrorMessage);
            if(model == null)
            {
                return RedirectToActionPermanent(BlogTypeRouteData + "/Archive", "Blog");
            }
            ModelState.RevalidateCommentFields(Model);

            if (ModelState.IsValid)
            {
                if (Model.Reference > 0)
                {

                    try
                    {
                        string strRegex = @"@(.*?):";
                        RegexOptions myRegexOptions = RegexOptions.None;
                        Regex myRegex = new Regex(strRegex, myRegexOptions);
                        string strTargetString = @"@123-name:";
                        string strReplace = @"<a href=""#comment-$1"">$2:</a>";

                        System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(strRegex).Matches(Model.Comment);

                        foreach (Match match in API)
                        {

                            int xid = 0;
                            string username = "";

                            MatchCollection ID = new Regex(@"([0-9]+)([-])(.*?)([:])").Matches(match.Value);

                            if (ID[0].Groups.Count == 5)
                            {
                                int.TryParse(ID[0].Groups[1].Value, out xid);
                                username = ID[0].Groups[3].Value;
                            }
                            if (xid > 0 && username != "")
                                Model.Comment = Model.Comment.Replace(match.Value,
                                "<a href=\"#comment-" + xid + "\">@" + username + ":</a> ");
                            else
                                Model.Comment = Model.Comment.Replace(match.Value,
                                       "");

                        }

                    }
                    catch (Exception e)
                    {
                    }

                }
                nContent add = this._AddComment( ref Model, refId: Model.Reference);
                if (add != null)
                {
                    add.GenerateLink();

                    return Redirect(add.FullLink + "#comment-" + Model.ScrollTo);
                }
                else
                {
                    ModelState.AddModelError("form", "Es ist ein Fehler beim absenden des Kommentars aufgetreten. Bitte versuch es erneut");
                }
            }

            model.PostedComment = Model;

            //Clearing Values
            if(ModelState["FormID"] != null)
            ModelState.SetModelValue("FormID", new ValueProviderResult("", "", CultureInfo.CurrentCulture));

            if(ModelState["Nickname"] != null)
                ModelState.SetModelValue("Nickname", new ValueProviderResult("", "", CultureInfo.CurrentCulture));

            if (ModelState["Comment"] != null)
                ModelState.SetModelValue("Comment", new ValueProviderResult("", "", CultureInfo.CurrentCulture));

            return View(model);
        }

        public ActionResult Tag(string tgn, int page = 1)
        {
            ContentManagement.GetContent GetC = new ContentManagement.GetContent();
            string[] t = new string[1];
            t[0] =ContentType;

            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            string realTag = tgn;
            string query = "SELECT COUNT(*) as tagCount, tagName FROM " + (new Ren.CMS.CORE.ThisApplication.ThisApplication().getSqlPrefix) + "Content_Tags WHERE tagNameSEO=@tagName AND contentType=@ct AND enableBrowsing=1 GROUP BY tagName";

            nSqlParameterCollection N = new nSqlParameterCollection();

            N.Add("@tagName", tgn);
            N.Add("@ct",ContentType);
            SqlDataReader R = SQL.SysReader(query, N);
            if (!R.HasRows) return RedirectToAction("Archive", "News");
            R.Read();

            int count = 0;

            if (R["tagCount"] != DBNull.Value) {

                count = (int)R["tagCount"];
                ViewBag.TagName = (string)R["tagName"];
                realTag = (string)R["tagName"];

            }
            R.Close();
            SQL.SysDisconnect();
            if (count == 0) return RedirectToAction(BlogTypeRouteData +"/Archive");

            GetC.GetContentByTag(realTag, t,null,"{prefix}Content.cDate","DESC",false,page,20);

            List<nContent> Li = GetC.getList();

            nPagingCollection pages = new nPagingCollection(GetC.TotalRows, 10);
            ViewData["TotalRows"] = GetC.TotalRows;
            ViewData["Show"] = Li.Count;
            ViewData["Pages"] = pages;
            ViewData["Page"] = page;
            ViewData["Entries"] = Li;

            return View();
        }

        private NewsDetail getDetailViewModel(int id, object page = null)
        {
            if (id == 0) return null;

            List<Ren.CMS.Content.nContent> Entry = new List<Ren.CMS.Content.nContent>();

            List<Ren.CMS.Content.nContent> Comments = new List<Ren.CMS.Content.nContent>();
            int totalRows = 0;
            if (id == 0) return null;
            else
            {

                int idd = id;

                if (idd == -1) return null;
                Ren.CMS.Content.ContentManagement.GetContent G = new Ren.CMS.Content.ContentManagement.GetContent(idd);
                Ren.CMS.Content.ContentManagement.GetContent C = new Ren.CMS.Content.ContentManagement.GetContent(new string[] { "eComment" }, languages: new string [] { Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage }, pageIndex: 1, pageSize: 10, contentRef: idd);

                Entry = G.getList();
                Comments = C.getList();
                Comments.ForEach(a => a.CreatorName = Ren.CMS.Blog.Helpers.NewsCommentHelper.SpecialNameForGuests(a));

                totalRows = C.TotalRows;
                if (Entry.Count == 0) RedirectToActionPermanent("Code/404", "Error");
                Entry[0].GenerateLink();
                if (Entry[0].ContentType !=ContentType) RedirectToActionPermanent(Entry[0].TargetAction, Entry[0].TargetController);

            }

            if (Entry.Count == 0)
                return null;

            NewsDetail DetailModel = new NewsDetail();

            DetailModel.NewsEntry = Entry.First();
            foreach (nContentText Text in DetailModel.NewsEntry.Texts)
            {

                if (Text.LangCode != Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage && DetailModel.NewsEntry.Texts.Any(e => e.LangCode == Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage))
                    DetailModel.NewsEntry.Texts.Remove(Text);
                else
                {
                     string l = Ren.CMS.CORE.Helper.CurrentLanguageHelper.DefaultLanguage;
                     if (DetailModel.NewsEntry.Texts.Any(e => e.LangCode == l))
                     {
                         if (Text.LangCode != l)
                             DetailModel.NewsEntry.Texts.Remove(Text);

                     }
                }

            }
            DetailModel.Comments = Comments;
            DetailModel.CommentsOnPage = Comments.Count();
            DetailModel.TotalComments = DetailModel.NewsEntry.GetCommentsCount();

            return DetailModel;
        }

        private nContent Map(NewsComment c)
        {
            if (Request.IsAuthenticated)
            {

                c.Nickname = null;
            }
            else
            {
                if (String.IsNullOrEmpty(c.Nickname) ||
                                String.IsNullOrWhiteSpace(c.Nickname))
                {

                    c.Nickname = "Besucher";

                }

            }

            ContentManagement.GetContent Check = new ContentManagement.GetContent(id: (c.Reference > 0 ? c.Reference : c.NewsID));
            var e = Check.getList().First();

            nContentText Text = new nContentText()
            {
                Id = 0,
                LangCode = Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage,
                LongText = c.Comment,
                PreviewText = c.Comment,
                MetaDescription = "",
                MetaKeyWords = "",
                SEOName = "",
                Title = "eComment"
            };

            List<nContentText> texts = new List<nContentText>() { Text };

            nContent Content = new nContent(
                id: -1,
                contentTexts: texts,
                type: "eComment",
                category: e.CategoryName,
                creatorPKid: MemberShip.Helper.CmsUser.Current.ProviderUserKey,
                CreatorSpecialName_: c.Nickname,
                username: MemberShip.Helper.CmsUser.Current.UserName,
                locked: false,
                 ratinggroupid: 0,
                cdate: DateTime.Now,
                Cref: (c.Reference > 0 ? c.Reference : c.NewsID),
                cid: e.CategoryID);

            return Content;
        }

        private nContent _AddComment(ref NewsComment Comment, int refId = 0)
        {
            int idForRef = Comment.NewsID;

            ContentManagement.GetContent Check = new ContentManagement.GetContent(id: Comment.NewsID);

            if (Check.getList().Where(e => e.ContentType ==ContentType || e.ContentType == "eComment").Count() == 0) return null;

            var element = Check.getList().First();
            element.GenerateLink();

            if (refId > 0)
            {
                //Check Ref exists:
                ContentManagement.GetContent CheckRef = new ContentManagement.GetContent(id: refId);
                if (CheckRef.getList().Where(e => e.ContentType ==ContentType || e.ContentType == "eComment").Count() == 0)

                    return null;

                 idForRef = refId;
            }

            nContent NewComment = this.Map(Comment);

            ContentManagement CM = new ContentManagement();

            bool success = CM.InsertContent(ref NewComment);

            Comment.ScrollTo = NewComment.ID;
            if (!success)
                return null;
            return element;
        }

        #endregion Methods
    }
}