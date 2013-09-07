using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ren.CMS.Content;

namespace GalleryModule.Gallery.Models
{

    public class GalleryNavigation
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public int Page { get; set; }
        [Required]
        public int ContentID { get; set; }


    }


    public class GalleryView
    {


        public string Type { get; set; }
        public string Reference { get; set; }
        public GalleryView(string reference, int contentID, string type, int page)
        {
            this.Type = type;
            this.Reference = reference;
            ContentManagement.GetContent Get = new ContentManagement.GetContent(id: contentID);
            var l = Get.getList();
            if (l.Count > 0)
            {
                this.Content = l.First();
                this.Content.GenerateLink();
            }

            var Attachments = this.Content.Attachments(type, "gallery");

            if (Attachments.Count < page)
            {
                page = 1;
            }

            this.Page = page;
            this.MaxPage = Attachments.Count;
            this.MinPage = 1;
            int index = page - 1;

            this.CurrentView = Attachments[index];

            if (type.ToLower() == "image")
            {

                this.LocaleType = "Weitere Bilder";

            }
            else if (type.ToLower() == "video")
            {
                this.LocaleType = "Videos";

            }





        }
        public nContent Content { get; set; }

        public nContent.nAttachment CurrentView
        {
            get;
            set;
        }

        public int Page { get; set; }
        public int MaxPage { get; set; }
        public int MinPage { get; set; }


        public string LocaleType { get; set; }

        public RedirectResult RedirectAction()
        {

            if (this.Content != null)
            {
                this.Content.GenerateLink();
                return new RedirectResult(this.Content.FullLink);


            }

            return new RedirectResult("/Home/");
        }

        public bool IsValid()
        {
            bool valid = (this.Content != null)
                            && (this.CurrentView != null)
                            && (this.Page != 0)
                            && (this.MinPage != 0)
                            && (this.MaxPage != 0);


            return valid;


        }


    }
}