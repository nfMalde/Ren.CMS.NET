namespace Ren.CMS.Article.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Security;

    using Ren.CMS.CORE.Language;

    public class ArticleComment
    {
        #region Properties

        public string nickname
        {
            get; set;
        }

        [Required]
        public string text
        {
            get; set;
        }

        #endregion Properties
    }

    public class ArticleListModel
    {
        #region Properties

        public string ArticleListImage
        {
            get; set;
        }

        public string Author
        {
            get; set;
        }

        public string Category
        {
            get; set;
        }

        public string PreviewText
        {
            get; set;
        }

        public string SEOLink
        {
            get; set;
        }

        public string SubCategory
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        #endregion Properties
    }

    public class ProContraList
    {
        #region Properties

        public List<string> name
        {
            get; set;
        }

        #endregion Properties
    }
}