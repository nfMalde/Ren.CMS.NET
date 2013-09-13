namespace Ren.CMS.Blog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using Ren.CMS.Content;
    using Ren.CMS.CORE.Language;
    using Ren.CMS.CORE.Settings;

    public class NewsArchive
    {
        #region Properties

        public Dictionary<DateTime, List<nContent>> News
        {
            get; set;
        }

        public int Page
        {
            get; set;
        }

        public int RowsOnPage
        {
            get; set;
        }

        public int TotalRows
        {
            get; set;
        }

        #endregion Properties
    }

    public class NewsComment
    {
        #region Properties

        [Required]
        public string Comment
        {
            get; set;
        }

        [Required]
        public string FormID
        {
            get; set;
        }

        [Required]
        public int NewsID
        {
            get; set;
        }

        [StringLength(200)]
        public string Nickname
        {
            get; set;
        }

        public int Reference
        {
            get; set;
        }

        public object RouteValues
        {
            get
            {

            if(this.TargetAction != null && this.TargetController != null)
            {
                return new { action=this.TargetAction, controller = this.TargetController, area = ""};

            }

            return null;
            }
        }

        public int ScrollTo
        {
            get; set;
        }

        public string TargetAction
        {
            get; set;
        }

        public string TargetController
        {
            get; set;
        }

        #endregion Properties
    }

    public class NewsCommentAnswerForm
    {
        #region Properties

        public int CommentID
        {
            get;
            set;
        }

        public NewsComment NewsComment
        {
            get; set;
        }

        #endregion Properties
    }

    public class NewsCommentAnswerView
    {
        #region Fields

        public int CommentSize = 10;
        public int Page = 1;

        #endregion Fields

        #region Properties

        public int CommentAnswerCountOnPage
        {
            get;
            set;
        }

        public IEnumerable<nContent> CommentAnswers
        {
            get {

                ContentManagement.GetContent AnswersGet = new ContentManagement.GetContent(acontenttypes: new string[] { "eComment" },
                                                                          contentRef: this.MainCommentID,
                                                                          pageIndex:1,
                                                                          pageSize: ( this.Page * this.CommentSize));
                GlobalSettings Gsettings = new GlobalSettings();

                var Guest = Gsettings.getSetting("GLOBAL_GUESTPKID").Value.ToString();

                var Answers =  AnswersGet.getList();
                Answers.ForEach(a => a.CreatorName = Ren.CMS.Blog.Helpers.NewsCommentHelper.SpecialNameForGuests(a));
                this.CommentAnswerCountOnPage = Answers.Count;
                this.CommentAnswersCountTotal = AnswersGet.TotalRows;
                return Answers;
            }
        }

        public int CommentAnswersCountTotal
        {
            get; set;
        }

        public int MainCommentID
        {
            get; set;
        }

        public nContent NewsEntry
        {
            get;  set;
        }

        #endregion Properties
    }

    public class NewsDetail
    {
        #region Fields

        public string mode = "normal";

        #endregion Fields

        #region Properties

        public int CommentPage
        {
            get; set;
        }

        public IEnumerable<nContent> Comments
        {
            get; set;
        }

        public int CommentsOnPage
        {
            get; set;
        }

        public string FormIDgotError
        {
            get; set;
        }

        public nContent NewsEntry
        {
            get; set;
        }

        public NewsComment PostedComment
        {
            get; set;
        }

        public int ScrollTo
        {
            get; set;
        }

        public int TotalComments
        {
            get; set;
        }

        public string UniqueUrl
        {
            get{

            Ren.CMS.CORE.Helper.LinkHelper.LinkHelper LinkHelper1 = new Ren.CMS.CORE.Helper.LinkHelper.LinkHelper();
               return  LinkHelper1.generateUniqueURL(this.NewsEntry);

            }
        }

        #endregion Properties
    }
}