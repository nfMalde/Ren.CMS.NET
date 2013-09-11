namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Category
    {
        #region Properties

        public virtual string ContentType
        {
            get; set;
        }

        public virtual string LongName
        {
            get; set;
        }

        public virtual System.Guid Pkid
        {
            get; set;
        }

        public virtual string ShortName
        {
            get; set;
        }

        public virtual string SubFrom
        {
            get; set;
        }

        #endregion Properties
    }
}