namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ArticleRating
    {
        #region Properties

        public virtual int ArticleID
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual int RatingID
        {
            get; set;
        }

        public virtual int Stars
        {
            get; set;
        }

        #endregion Properties
    }
}