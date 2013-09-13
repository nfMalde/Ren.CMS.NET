namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ThumpnailsModule
    {
        #region Properties

        public virtual string AtID
        {
            get; set;
        }

        public virtual System.Nullable<int> Height
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual System.DateTime LastModification
        {
            get; set;
        }

        public virtual string Path
        {
            get; set;
        }

        public virtual System.Nullable<int> Width
        {
            get; set;
        }

        #endregion Properties
    }
}