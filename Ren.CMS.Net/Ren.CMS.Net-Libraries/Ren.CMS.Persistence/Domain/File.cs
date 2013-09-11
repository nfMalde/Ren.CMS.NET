namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class File
    {
        #region Properties

        public virtual int Active
        {
            get; set;
        }

        public virtual string AliasName
        {
            get; set;
        }

        public virtual System.Nullable<int> FileSize
        {
            get; set;
        }

        public virtual string Fpath
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string NeedPermission
        {
            get; set;
        }

        public virtual System.Nullable<int> ProfileID
        {
            get; set;
        }

        #endregion Properties
    }
}