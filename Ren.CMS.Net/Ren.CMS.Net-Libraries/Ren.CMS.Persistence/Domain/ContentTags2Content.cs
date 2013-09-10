namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentTags2Content
    {
        #region Properties

        public virtual int ContentID
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual int TagID
        {
            get; set;
        }

        #endregion Properties
    }
}