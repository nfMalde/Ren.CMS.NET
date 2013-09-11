namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SubCategory
    {
        #region Properties

        public virtual System.Nullable<System.Guid> Cid
        {
            get; set;
        }

        public virtual string LangCode
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

        public virtual string Ref
        {
            get; set;
        }

        public virtual string ShortName
        {
            get; set;
        }

        #endregion Properties
    }
}