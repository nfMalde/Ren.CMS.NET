namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FilemanagementControllersAcceptMimeType
    {
        #region Properties

        public virtual System.Nullable<int> Cid
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string MimeType
        {
            get; set;
        }

        #endregion Properties
    }
}