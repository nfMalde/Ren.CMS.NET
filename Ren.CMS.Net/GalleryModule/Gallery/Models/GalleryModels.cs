namespace GalleryModule.Gallery.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.Content;

    public class GalleryNavigation
    {
        #region Properties

        [Required]
        public int ContentID
        {
            get; set;
        }

        [Required]
        public int Page
        {
            get; set;
        }

        [Required]
        public string Type
        {
            get; set;
        }

        #endregion Properties
    }

    public class GalleryView
    {
        #region Constructors

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

        #endregion Constructors

        #region Properties

        public nContent Content
        {
            get; set;
        }

        public nContent.nAttachment CurrentView
        {
            get;
            set;
        }

        public string LocaleType
        {
            get; set;
        }

        public int MaxPage
        {
            get; set;
        }

        public int MinPage
        {
            get; set;
        }

        public int Page
        {
            get; set;
        }

        public string Reference
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public bool IsValid()
        {
            bool valid = (this.Content != null)
                            && (this.CurrentView != null)
                            && (this.Page != 0)
                            && (this.MinPage != 0)
                            && (this.MaxPage != 0);

            return valid;
        }

        public RedirectResult RedirectAction()
        {
            if (this.Content != null)
            {
                this.Content.GenerateLink();
                return new RedirectResult(this.Content.FullLink);

            }

            return new RedirectResult("/Home/");
        }

        #endregion Methods
    }
}