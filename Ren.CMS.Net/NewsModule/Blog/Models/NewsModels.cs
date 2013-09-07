using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Ren.CMS.CORE.Language;
using System.ComponentModel;
using Ren.CMS.Content;
using System.Web;
using Ren.CMS.CORE.Settings;
using System.Linq;

namespace Ren.CMS.Blog.Models
{


    public class NewsComment
    {
        [Required]
        public int NewsID { get; set; }

        [StringLength(200)]
        public string Nickname { get; set; }
        [Required]
        public string Comment { get; set; }

        public int Reference { get; set; }

        public int ScrollTo { get; set; }

        public string TargetAction { get; set; }
        public string TargetController { get; set; }

        public object RouteValues { 
            get 
            {

            if(this.TargetAction != null && this.TargetController != null)
            {
                return new { action=this.TargetAction, controller = this.TargetController, area = ""};

            }
        
            return null;
            }
        
        
        }
        
        [Required]
        public string FormID { get; set; }
    }
 

    public class NewsDetail
    {
        public string mode = "normal";
        public NewsComment PostedComment { get; set; }
        public nContent NewsEntry { get; set; }
        public int TotalComments { get; set; }
        public int CommentsOnPage { get; set; }
        public int CommentPage { get; set; }
        public string UniqueUrl { get{

            Ren.CMS.CORE.Helper.LinkHelper.LinkHelper LinkHelper1 = new Ren.CMS.CORE.Helper.LinkHelper.LinkHelper();
           return  LinkHelper1.generateUniqueURL(this.NewsEntry);

        
        }}

        public IEnumerable<nContent> Comments { get; set; }

        public int ScrollTo { get; set; }
        public string FormIDgotError { get; set; }
    }

 

    public class NewsCommentAnswerView
    {
        
     
        public int CommentSize = 10;

        public int Page = 1;

        public nContent NewsEntry { get;  set; }

        public int MainCommentID { get; set; }

        public IEnumerable<nContent> CommentAnswers {

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
        public int CommentAnswerCountOnPage
        {

            get;
            set;

        }

        public int CommentAnswersCountTotal { get; set; }
    }

    public class NewsCommentAnswerForm
    {

        public int CommentID
        {

            get;
            set;

        }

        public NewsComment NewsComment { get; set; }

    }

    public class NewsArchive
    {

        public Dictionary<DateTime,List<nContent>> News { get; set; }

        public int TotalRows { get; set; }

        public int RowsOnPage { get; set; }

        public int Page { get; set; }

    
    }
}
